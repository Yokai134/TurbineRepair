using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using static System.Net.WebRequestMethods;

namespace TurbineRepair.ViewModel
{
    internal class CreateOrUpdateEmployee : Base.ViewModel
    {

        #region Prop

        #region SelectPost
        private Post _selectPost;
        public Post SelectPost
        {
            get=> _selectPost; 
            set => Set(ref _selectPost, value);
        }
        #endregion

        #region SelectRole
        private Role _selectRole;
        public Role SelectRole
        {
            get => _selectRole;
            set => Set(ref _selectRole, value);
        }
        #endregion

     

        #endregion


        #region UserField


        private string _surname;
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _patronomyc;
        public string Patronomyc
        {
            get => _patronomyc;
            set => Set(ref _patronomyc, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set=> Set(ref _phone, value); 
        }

        private string _login;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        private string _pin;
        public string Pin
        {
            get=> _pin; 
            set => Set(ref _pin, value);
        }

        #region ImagePath
        private string? _imagePath = "https://i.imgur.com/NAGTvvz.png";
        public string? ImagePath
        {
            get => _imagePath;
            set => Set(ref _imagePath, value);
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
                UserDatum updUserDatum = MainWindowViewModel.main.UpdateUser;
                updUserDatum.Name = Name;
                updUserDatum.Surname = Surname;
                updUserDatum.Patronomyc = Patronomyc;
                updUserDatum.Role = SelectRole.Id;
                updUserDatum.Post = SelectPost.Id;
                updUserDatum.Phone = Phone;
                updUserDatum.Login = Login;
                updUserDatum.Password = Password;
                updUserDatum.Pincode = Pin;
                updUserDatum.Image = ImagePath;
                MainWindowViewModel.context.SaveChanges();
                await MainWindowViewModel.main.UpdateData();
                MessageBox.Show("Данные пользователя обновленны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (SelectPost != null && SelectRole != null && Surname != null && Name != null
                    && Patronomyc != null && Phone != null && Login != null && Password != null 
                    && Pin != null && ImagePath != null)
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
                    newUserDatum.Pincode = Pin;
                    newUserDatum.Image = ImagePath;
                    MainWindowViewModel.context.UserData.Add(newUserDatum);
                    MainWindowViewModel.context.SaveChanges();
                    await MainWindowViewModel.main.UpdateData();
                    MessageBox.Show("Пользователь создан", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Не выбрана роль, должность, или же не все поля заполнены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
                
           
        }
        #endregion

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
                Pin = MainWindowViewModel.main.UpdateUser.Pincode;
                ImagePath = MainWindowViewModel.main.UpdateUser.Image;
            }

            AddUser = new LambdaCommand(OnAddUserExecute, CanAddUserExecute);

            PathImage = new LambdaCommand(OnPathImageExecute, CanPathImageExecute);

        }
    }
}
