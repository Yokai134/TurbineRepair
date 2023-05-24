using Microsoft.Win32;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;
using static System.Net.WebRequestMethods;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateEmployee : Base.ViewModel , INotifyDataErrorInfo
    {
        private string patternRegex = "^[a-zA-Zа-яА-Я]+$";
        private string phonePattern = "^(\\(?\\+?[0-9]*\\)?)?[0-9_\\- \\(\\)]*$";
        private string loginPatttern = "^(?=[a-zA-Z0-9._]{8,32}$)(?!.*[_.]{2})[^_.].*[^_.]$";
        private string passPattern = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,16}$";

        #region Prop

        private decimal _foregroundFailedMessage;
        public decimal ForegroundFailedMessage
        {
            get => _foregroundFailedMessage;
            set => Set(ref _foregroundFailedMessage, value);
        }
        private string _failedAddOrUpdateContent;
        public string FailedAddOrUpdateContent
        {
            get => _failedAddOrUpdateContent;
            set => Set(ref _failedAddOrUpdateContent, value);
        }

        #region SelectPost
        private Post _selectPost;
        public Post SelectPost
        {
            get => _selectPost;
            set
            {
                Set(ref _selectPost, value);
                ValidatePost();
            }
        }
        #endregion

        #region SelectRole
        private Role _selectRole;
        public Role SelectRole
        {
            get => _selectRole;
            set
            {
                Set(ref _selectRole, value);
                ValidateRole();
            }  
        }
        #endregion

        #region ContentButton
        private string _contentButton;
        public string ContentButton
        {
            get => _contentButton;
            set => Set(ref _contentButton, value);
        }
        #endregion

        #region BoolValidate
        private bool _checkSurname = false;
        public bool CheckSurname
        {
            get => _checkSurname;
            set => Set(ref _checkSurname, value);
        }
        private bool _checkName = false;
        public bool CheckName
        {
            get => _checkName;
            set => Set(ref _checkName, value);
        }
        private bool _checkPatronomyc = false;
        public bool CheckPatronomyc
        {
            get => _checkPatronomyc;
            set => Set(ref _checkPatronomyc, value);
        }

        private bool _checkPhone = false;
        public bool CheckPhone
        {
            get => _checkPhone;
            set => Set(ref _checkPhone, value);
        }
        private bool _checkLogin = false;
        public bool CheckLogin
        {
            get => _checkLogin;
            set => Set(ref _checkLogin, value);
        }
        private bool _checkPassword = false;
        public bool CheckPassword
        {
            get => _checkPassword;
            set => Set(ref _checkPassword, value);
        }
        private bool _checkImage = false;
        public bool CheckImage
        {
            get => _checkImage;
            set => Set(ref _checkImage, value);
        }
        private bool _checkRole = false;
        public bool CheckRole
        {
            get => _checkRole;
            set => Set(ref _checkRole, value);
        }
        private bool _checkPost = false;
        public bool CheckPost
        {
            get => _checkPost;
            set => Set(ref _checkPost, value);
        }
        #endregion

        #endregion

        #region UserField


        private string _surname;
        public string Surname
        {
            get => _surname;
            set 
            {
                Set(ref _surname, value);
                ValidateSurname();
            } 
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
                ValidateName();
            }
        }

        private string _patronomyc;
        public string Patronomyc
        {
            get => _patronomyc;
            set
            {
                Set(ref _patronomyc, value);
                ValidatePatronomyc();
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set 
            { 
                Set(ref _phone, value); 
                ValidationPhone(); 
            }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                Set(ref _login, value);
                ValidationLogin();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                ValidationPassword();
            }
        }


        #region ImagePath
        private string? _imagePath = "https://i.imgur.com/NAGTvvz.png";
        public string? ImagePath
        {
            get => _imagePath;
            set
            {
                Set(ref _imagePath, value);
                ValidateImage();
            }
        }
        #endregion

        #endregion

        #region List

        #region PostList
        private List<Post> _postsList = MainWindowViewModel.main.PostsAll.ToList();
        public List<Post> PostsList
        {
            get => _postsList;
            set => Set(ref _postsList, value);
        }
        #endregion

        #region RoleList
        private List<Role> _roleList = MainWindowViewModel.main.RoleAll.ToList();
        public List<Role> RoleList
        {
            get => _roleList;
            set => Set(ref _roleList, value);
        }
        #endregion

        #endregion


        #region Command

        #region AddUser
        public ICommand AddUser { get; }

        private bool CanAddUserExecute(object parameter) => true;

        private async void OnAddUserExecute(object parametr)
        {
            if (MainWindowViewModel.main.UpdateUser != null)
            {
                ValidateName();
                ValidateSurname();
                ValidatePatronomyc();
                ValidateImage();
                ValidatePost();
                ValidateRole();
                ValidationPhone();
                ValidationLogin();
                ValidationPassword();
                if(CheckName && CheckSurname && CheckPatronomyc && CheckRole && CheckPost && CheckPhone && CheckLogin && CheckPassword && CheckImage)
                {
                    UserDatum updUserDatum = MainWindowViewModel.main.UpdateUser;
                    updUserDatum.Name = Name;
                    updUserDatum.Surname = Surname;
                    updUserDatum.Patronomyc = Patronomyc;
                    updUserDatum.Role = SelectRole.Id;
                    updUserDatum.Post = SelectPost.Id;
                    updUserDatum.Phone = Phone;
                    updUserDatum.Login = Login;
                    updUserDatum.Password = Password;
                    updUserDatum.Image = ImagePath;
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    FailedAddOrUpdateContent = "Данные обновлены";
                    ForegroundFailedMessage = 1;


                }
                else
                {
                    FailedAddOrUpdateContent = "*Не удалось обновить данные";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
 
            }
            else
            {
                ValidateName();
                ValidateSurname();
                ValidatePatronomyc();
                ValidateImage();
                ValidatePost();
                ValidateRole();
                ValidationPhone();
                ValidationLogin();
                ValidationPassword();
                if (CheckName && CheckSurname && CheckPatronomyc && CheckRole && CheckPost && CheckPhone && CheckLogin && CheckPassword && CheckImage)
                {
                    UserDatum newUserDatum = new UserDatum();
                    newUserDatum.Name = Name;
                    newUserDatum.Surname = Surname;
                    newUserDatum.Patronomyc = Patronomyc;
                    newUserDatum.Role = SelectRole.Id;
                    newUserDatum.Post = SelectPost.Id;
                    newUserDatum.Phone = Phone;
                    newUserDatum.Login = Login;
                    newUserDatum.Password = Password;
                    newUserDatum.Image = ImagePath;
                    newUserDatum.IsOnline = false;
                    newUserDatum.DeleteUser = false;
                    MainWindowViewModel.context.UserData.Add(newUserDatum);
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    ForegroundFailedMessage = 1;
                    FailedAddOrUpdateContent = "*Новый сотрудник добавлен";
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshContent;
                    timer.Start();

                }
                else
                {
                    FailedAddOrUpdateContent = "*Не удалось добавить данные";
                    ForegroundFailedMessage = -1;
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += RefreshValidator;
                    timer.Start();
                }
            }
                
           
        }
        #endregion

        public ICommand BackEmployeeList { get; }
        private bool CanBackEmployeeListExecute(object parametr) => true;
        private void OnBackEmployeeListExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new EmployeeVM();
        }

        #region PathImage
        public ICommand PathImage { get; }

        private bool CanPathImageExecute(object parameter) => true;
        private void OnPathImageExecute(object parametr)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";          
            openFileDialog.ShowDialog();
            ImagePath = openFileDialog.FileName;
        }
        #endregion

        #endregion

        public CreateOrUpdateEmployee()
        {
            if(MainWindowViewModel.main.UpdateUser != null)
            {
                Surname = MainWindowViewModel.main.UpdateUser.Surname;
                Name = MainWindowViewModel.main.UpdateUser.Name;
                Patronomyc = MainWindowViewModel.main.UpdateUser.Patronomyc;
                SelectRole = MainWindowViewModel.main.UpdateUser.RoleNavigation;
                SelectPost = MainWindowViewModel.main.UpdateUser.PostNavigation;
                Phone = MainWindowViewModel.main.UpdateUser.Phone;
                Login = MainWindowViewModel.main.UpdateUser.Login;
                Password = MainWindowViewModel.main.UpdateUser.Password;
                ImagePath = MainWindowViewModel.main.UpdateUser.Image;
                ContentButton = "Изменить";
            }
            else ContentButton = "Добавить";

            #region Command
            AddUser = new LambdaCommand(OnAddUserExecute, CanAddUserExecute);
            PathImage = new LambdaCommand(OnPathImageExecute, CanPathImageExecute);
            BackEmployeeList = new LambdaCommand(OnBackEmployeeListExecute, CanBackEmployeeListExecute);
            #endregion
        }


        #region Validation

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }


        private void ValidateSurname()
        {
            ClearErrors(nameof(Surname));
            if (string.IsNullOrWhiteSpace(Surname) || !Regex.IsMatch(Surname, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(Surname));
                AddError(nameof(Surname), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckSurname = false;
            }
            else
            {
                CheckSurname = true;
            }

        }
        private void ValidateName()
        {
            ClearErrors(nameof(Name));
            if (string.IsNullOrWhiteSpace(Name) || !Regex.IsMatch(Name, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(Name));
                AddError(nameof(Name), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckName = false;

            }
            else
            {
                CheckName = true;
            }

        }
        private void ValidatePatronomyc()
        {
            ClearErrors(nameof(Patronomyc));
            if (string.IsNullOrWhiteSpace(Patronomyc) || !Regex.IsMatch(Patronomyc, patternRegex, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(Patronomyc));
                AddError(nameof(Patronomyc), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckPatronomyc = false;

            }
            else
            {
                CheckPatronomyc = true;
            }

        }

        private void ValidationPhone()
        {
            ClearErrors(nameof(Phone));
            if (string.IsNullOrWhiteSpace(Phone) || !Regex.IsMatch(Phone, phonePattern, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(Phone));
                AddError(nameof(Phone), "*Поле не может быть пустым, или в поле не допустимые смволы.");
                CheckPhone = false;

            }
            else
            {
                CheckPhone = true;
            }
        }

        private void ValidationLogin()
        {
            ClearErrors(nameof(Login));
            if (string.IsNullOrWhiteSpace(Login) || !Regex.IsMatch(Login, loginPatttern, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(Login));
                AddError(nameof(Login), "*Поле не может быть пустым, в поле не допустимые смволы, размер логина от 8 до 32 символов.");
                CheckLogin = false;

            }
            else
            {
                CheckLogin = true;
            }
        }

        private void ValidationPassword() 
        {
            ClearErrors(nameof(Password));
            if (string.IsNullOrWhiteSpace(Password) || !Regex.IsMatch(Password, passPattern, RegexOptions.IgnoreCase))
            {
                ClearErrors(nameof(Password));
                AddError(nameof(Password), "*Поле не может быть пустым, минимум одна буква, цифра, символ. Длинна пароля от 8 до 16 символов.");
                CheckPassword = false;

            }
            else
            {
                CheckPassword = true;
            }
        }

        private void ValidateImage()
        {
            ClearErrors(nameof(ImagePath));
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                ClearErrors(nameof(ImagePath));
                AddError(nameof(ImagePath), "*Поле не может быть пустым.");
                CheckImage = false;

            }
            else
            {
                CheckImage = true;
            }
        }

        private void ValidateRole()
        {
            ClearErrors(nameof(SelectRole));
            if (SelectRole == null)
            {
                ClearErrors(nameof(SelectRole));
                AddError(nameof(SelectRole), "*Не выбрана роль.");
                CheckRole = false;
            }
            else
            {
                CheckRole = true;
            }

        }


        private void ValidatePost()
        {
            ClearErrors(nameof(SelectPost));
            if ((SelectPost) == null)
            {
                ClearErrors(nameof(SelectPost));
                AddError(nameof(SelectPost), "*Не выбрана должность.");
                CheckPost = false;
            }
            else
            {
                CheckPost = true;
            }

        }



        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }


        #endregion

        #region DispatherTimer
        DispatcherTimer timer = new DispatcherTimer();
        private void RefreshValidator(object e, object sender)
        {
            ForegroundFailedMessage = 0;
            FailedAddOrUpdateContent = "";
            ClearErrors(nameof(Surname));
            ClearErrors(nameof(Name));
            ClearErrors(nameof(Patronomyc));
            ClearErrors(nameof(SelectPost));
            ClearErrors(nameof(SelectRole));
            ClearErrors(nameof(Phone));
            ClearErrors(nameof(Login));
            ClearErrors(nameof(Password));
            timer.Stop();
        }
        private void RefreshContent(object e, object sender)
        {
            CheckName = false;
            CheckImage = false;
            CheckSurname = false;
            CheckPatronomyc = false;
            CheckPost = false;
            CheckRole = false;
            CheckLogin = false;
            CheckPassword = false;
            Surname = "";
            Name = "";
            Patronomyc = "";
            SelectRole = null;
            SelectPost = null;
            Phone = "";
            Login = "";
            Password = "";
            FailedAddOrUpdateContent = string.Empty;
            timer.Stop();
        }
        private void OpenEmployeeList(object sender, object e)
        {
            MainVM.mainVM.MainCurrentControl = new EmployeeVM();
            timer.Stop();
        }
        #endregion
    }
}
