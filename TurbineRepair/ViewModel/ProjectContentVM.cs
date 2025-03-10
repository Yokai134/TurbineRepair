﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using TurbineRepair.Infrastructure;
using TurbineRepair.Model;

namespace TurbineRepair.ViewModel
{
    internal class ProjectContentVM : Base.ViewModel
    {

        #region Property

        #region HelperClass
        public class ImageSource
        {
            public byte[] imageSource { get; set; }
        }

        public class DataGridInfo
        {
            public string ProductName { get; set; }
            public string Unit { get; set; }
            public int ProductCount { get; set; }
            public double ProductCost { get; set; }
            public double NDS { get; set; }
            public double ProductCostNDS { get; set; }

            double CostNds(double ProductCost)
            {
                return ProductCost * 0.10;
            }

            double CostSum(double ProductCost, double NDS)
            {
                return NDS + ProductCost;
            }
        }

        #endregion

        #region List
        public List<ProjectDatum> Projects { get; set; }
        public List<ImageSource> ImageSources { get; set; }
        public List<TurbineRequest> TurbineRequests { get; set; }
        #endregion

        #region ContractInfo

        private string _headerContract;
        public string HeaderContract
        {
            get=> _headerContract; 
            set => Set(ref _headerContract, value);
        }

        private string _firstParagraph;
        public string FirstParagraph
        {
            get => _firstParagraph;
            set => Set(ref _firstParagraph, value);
        }

        private string _twoParagraph;
        public string TwoParagraph
        {
            get => _twoParagraph;
            set => Set(ref _twoParagraph, value);
        }

        private string _threeParagraph;
        public string ThreeParagraph
        {
            get => _threeParagraph;
            set => Set(ref _threeParagraph, value);
        }

        private string _fourParagraph;
        public string FourParagraph
        {
            get => _fourParagraph;
            set => Set(ref _fourParagraph, value);
        }

        private string _fiveParagraph;
        public string FiveParagraph
        {
            get => _fiveParagraph; 
            set => Set(ref _fiveParagraph, value);
        }

        private string _sixParagraph;
        public string SixParagraph
        {
            get => _sixParagraph; 
            set => Set(ref _sixParagraph, value);
        }

        private string _sevenParagraph;
        public string SevenParagraph
        {
            get => _sevenParagraph;
            set => Set(ref _sevenParagraph, value);
        }

        private List<DataGridInfo> _gridInfo = new List<DataGridInfo>();
        public List<DataGridInfo> GridInfo
        {
            get => _gridInfo;
            set => Set(ref _gridInfo, value);
        }

        #endregion

        #endregion

        #region Command
        public ICommand BackProjectList {  get; }
        private bool CanBackProjectListExecute(object parameter) => true;
        private void OnBackProjectListExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new ProjectVM();
        }
        #endregion

        public ProjectContentVM() 
        {
            try
            {
                Projects = MainWindowViewModel.main.ProjectData.Where(x => x.Id == MainWindowViewModel.main.CurrentProject.Id).ToList();
                TurbineRequests = MainWindowViewModel.main.TurbineRequests.Where(x => x.TurbineRequestProjectNavigation.Id == MainWindowViewModel.main.CurrentProject.Id).ToList();
                ImageSources = new List<ImageSource>();
                Task task = CreateContract();
                Task.Run(() => ImageAdd());
             
                #region Command
                BackProjectList = new LambdaCommand(OnBackProjectListExecute, CanBackProjectListExecute);
                #endregion
            }
            catch
            {
                FailedAddOrUpdate = "*Не удалось загрузить данные. Откройте проект заново";
                ForegroundFailedMessage = -1;

            }

        }


        protected async Task ImageAdd()
        {
            try
            {
                for (int i = 0; i < MainWindowViewModel.main.TurbineImage.Count; i++)
                {
                    if (MainWindowViewModel.main.TurbineImage[i].Id == MainWindowViewModel.main.CurrentProject.ProjectTurbineNavigation.TurbineImage)
                    {
                        ImageSources.Add(new ImageSource()
                        {
                            imageSource = MainWindowViewModel.main.TurbineImage[i].ImageOne
                        });

                        ImageSources.Add(new ImageSource()
                        {
                            imageSource = MainWindowViewModel.main.TurbineImage[i].ImageTwo
                        });

                        ImageSources.Add(new ImageSource()
                        {
                            imageSource = MainWindowViewModel.main.TurbineImage[i].ImageThree
                        });

                        ImageSources.Add(new ImageSource()
                        {
                            imageSource = MainWindowViewModel.main.TurbineImage[i].ImageFour
                        });

                    }

                }
            }
            catch
            {
                FailedAddOrUpdate = "*Не удалось загрузить данные. Откройте проект заново";
                ForegroundFailedMessage = -1;
            }
           
        }
        protected async Task CreateContract()
        {
            try
            {

                await Task.Run(() => HeaderContract = Projects[0].ProjectCustomerNavigation.CustomerOrganizationNavigation.OraganizationName + ", именуемый в дальнейшем «Заказчик», в лице " +
                    Projects[0].ProjectCustomerNavigation.CustomerSurname + " " + Projects[0].ProjectCustomerNavigation.CustomerName + " " + Projects[0].ProjectCustomerNavigation.CustomerPatronomyc + ", " +
                    "действующего на основании доверенности от 24.03.2014 г. №5 , с одной стороны, и TurbineRepair, именуемое в дальнейшем «Исполнитель», в лице " +
                    Projects[0].ProjectExecutorNavigation.Surname + " " + Projects[0].ProjectExecutorNavigation.Name + " " + Projects[0].ProjectExecutorNavigation.Patronomyc +
                    ", совместно именуемые «Стороны», " +
                    "с соблюдением требований Гражданского кодекса Российской Федерации, иных актов законодательства Российской Федерации и Положения о закупках товаров, работ, " +
                    "услуг для обеспечения деятельности Организация, " +
                    "на основании результатов запроса котировок, заключили настоящий договор (далее - Договор) о нижеследующем: ");

                await Task.Run(() => FirstParagraph = "1.1. По настоящему Договору Исполнитель обязуется изготовить   персонализированную   деловую и канцелярскую   продукцию (далее - Работы) по заданию Заказчика, " +
                    "а также осуществить поставку указанной продукции в порядке и в сроки, предусмотренные настоящим Договором. " +
                    "Работы выполняются с использованием материалов Исполнителя. " +
                    "\r\n1.2. Заказчик обязуется принять результат Работ и оплатить его. " +
                    "\r\n1.3. Виды, количество изготавливаемой продукции, ее стоимость, сроки выполнения Работ определяются в приложениях к Договору, которые являются его неотъемлемой частью. " +
                    "\r\n1.4. Заказчик, до начала выполнения Работ, предусмотренных Договором, передает Исполнителю исходный материал (т.е. образцы или материал в электронном виде, на основании которых " +
                    "Исполнитель изготавливает оригинал-макет), " +
                    "качество которого позволяет добиться выполнения Работ. Исходный материал (рисунок, текст и др.), передается путем его отправки на электронный адрес Исполнителя, указанный в разделе 7 Договора, " +
                    "либо путем передачи образца на бумажном носителе с проставлением на нем уполномоченными лицами Заказчика и Исполнителя отметок о его приеме-передаче. " +
                    "\r\n1.5. Нанесение на изготовленную продукцию корпоративной символики – эмблемы Заказчика, осуществляется в соответствии с утвержденными Заказчиком эскизами. " +
                    "Способ нанесения на продукцию корпоративной символики в соответствии со Спецификацией на изготовление продукции (приложение № 1). " +
                    "\r\n1.6. Исполнитель выполняет Работы своими силами или привлекает для этого третьих лиц, за действия/бездействие которых Исполнитель несет ответственность, как за свои собственные. " +
                    "\r\n1.7. Место поставки изготовленной по настоящему Договору продукции: " + Projects[0].ProjectCustomerNavigation.CustomerOrganizationNavigation.OraganizationAdres +
                    "\r\n1.8. Моментом поставки изготовленной продукции (результата Работ) является момент подписания Заказчиком Акта сдачи-приемки выполненных работ. " +
                    "\r\n1.9. Настоящий Договор вступает в силу с даты его подписания Сторонами и действует до исполнения Сторонами своих обязательств, принятых по нему. \r\n");

                await Task.Run(() => TwoParagraph = "2.1. Цена Работ по настоящему Договору определяется в Спецификации на изготовление продукции (приложение № 1) и указывается в счетах Исполнителя и товарных накладных. " +
                    "\r\n2.2. Цена Работ включает в себя компенсацию издержек Исполнителя и причитающееся ему вознаграждение, в том числе стоимость материалов, стоимость выполнения Работ, стоимость доставки продукции к месту поставки, транспортные расходы. " +
                    "\r\n2.3. Расчеты по настоящему Договору производятся с условием предоплаты в размере 50 (Пятидесяти) процентов от Цены Работ, на основании счета Исполнителя в течение 5 (Пяти) банковских дней с момента получения Заказчиком указанного счета. " +
                    "\r\n2.4. Окончательный расчет за выполненные Работы производится Заказчиком в течение 5 (Пяти) банковских дней с момента поставки изготовленной продукции (результата Работ) и получения заказчиком соответствующего счета и счета-фактуры. " +
                    "\r\n2.5. Исполнитель обязуется передать Заказчику счет на оплату 50 (Пятидесяти) процентов от Цены Работ в день подписания настоящего Договора. " +
                    "\r\n2.6. Датой платежа считается дата списания денежных средств на корреспондентский счет обслуживающего банка Исполнителя. \r\n");

                await Task.Run(() => ThreeParagraph = "3.1. В течение всего срока действия настоящего Договора Исполнитель обязуется: " +
                    "\r\nа) выполнить Работы надлежащим образом в полном объеме и в сроки, согласованные \r\nСторонами, в соответствии с настоящим Договором; " +
                    "\r\nб) обеспечить высокое качество выполняемых Работ; " +
                    "\r\nв) обеспечить устойчивое нанесение на продукцию корпоративной символики – эмблемы \r\nЗаказчика; " +
                    "\r\nг) своевременно информировать Заказчика о любых сложностях и затруднениях, мешающих полноценному и своевременному выполнению своих обязанностей по настоящему Договору; что не освобождает Исполнителя от выполнения обязанностей по-настоящему \r\nДоговору;  " +
                    "\r\nд) сообщать Заказчику, по его требованию все сведения о ходе исполнения своих \r\nобязанностей по настоящему Договору; " +
                    "\r\nе) соблюдать нормы профессиональной этики; " +
                    "\r\nж) не разглашать и не способствовать разглашению конфиденциальной информации, \r\nполученной в ходе исполнения обязательств по настоящему Договору, третьим лицам; " +
                    "\r\nз) по факту выполнения Работ своевременно оформить и представить Заказчику в день \r\nпоставки продукции акт сдачи-приемки выполненных работ (далее – Акт, Акты), товарные накладные и соответствующие счета-фактуры; " +
                    "\r\nи) осуществить поставку изготовленной продукции (результата Работ) по адресу Заказчика, указанному в п. 1.7 Договора в установленный в Графике выполнения работ и поставки изготовленной продукции срок; " +
                    "\r\nк) в случае привлечения к исполнению настоящего Договора третьих лиц, обеспечить надлежащее исполнение указанными третьими лицами условий Договора, соблюдение ими установленных Договором сроков, нести ответственность за любое неисполнение третьими лицами настоящего Договора как за свое собственное; " +
                    "\r\nл) в случае изготовления и поставки продукции ненадлежащего качества, либо в количестве, не соответствующем Договору, Исполнитель обязуется в срок, согласованный с Заказчиком, изготовить и поставить продукцию, качество и/или количество которой отвечает требованиям Договора. " +
                    "\r\n3.2. В течение всего срока действия настоящего Договора Заказчик обязуется: " +
                    "\r\nа) не разглашать и не способствовать разглашению конфиденциальной информации, \r\nполученной в ходе исполнения обязательств по настоящему Договору; " +
                    "\r\nб) оплачивать выполненные Работы в порядке, в сроки и в размере, установленные \r\nнастоящим Договором. " +
                    "\r\nв) предоставлять Исполнителю всю информацию, необходимую для выполнения Работ, с \r\nприложением необходимой документации.  \r\n");

                await Task.Run(() => FourParagraph = "4.1. Срок выполнения Работ и поставки изготовленной продукции (результата Работ) по " + Projects[0].ProjectDataEnd +
                    "\r\n4.2. Заказчик принимает изготовленную продукцию (выполненные Работы) по товарной накладной и акту сдачи-приемки выполненных работ, а при обнаружении отступлений от условий Договора (согласованных сторонами в спецификации), ухудшающих изготовленную продукцию (выполненные Работы) составляет акт с участием представителя Исполнителя. " +
                    "\r\nИсполнитель обязан передать, а Заказчик принять изготовленную продукцию (выполненные \r\nРаботы) в течение 3 дней с момента соответствующего окончания изготовления продукции (выполнения Работ). " +
                    "\r\n4.3. Заказчик, обнаруживший недостатки в изготовленной продукции (выполненных Работ) при ее приемке, вправе ссылаться на них только в случаях, если в акте приемки были зафиксированы эти недостатки либо возможность последующего предъявления требования об их устранении. " +
                    "\r\n4.4. При возникновении между Заказчиком и Исполнителем спора по поводу недостатков изготовленной продукции (выполненных Работ) или их причин по требованию любой из Сторон должна быть назначена экспертиза. Расходы по проведению экспертизы несет Сторона, потребовавшая назначения экспертизы. В случае установления экспертизой невиновности этой Стороны в недостатках изготовленной продукции (выполненных Работ), расходы по ее проведению возлагаются на контрагента. " +
                    "\r\n4.5. Одновременно с передачей Исполнителем Заказчику изготовленной продукции (результата Работ) Исполнитель обязуется передать Заказчику соответствующий счет на оплату Работ и счет-фактуру. \r\n");

                await Task.Run(() => FiveParagraph = "5.1. В случае, когда Работы выполнены Исполнителем с отступлениями от условий настоящего Договора (согласованных сторонами в спецификации на изготовление продукции) или с иными недостатками, которые делают его не пригодным для использования в установленных Заказчиком целях, Заказчик вправе по своему выбору потребовать от Исполнителя: " +
                    "\r\n - безвозмездного устранения недостатков в срок, определенный Заказчиком; " +
                    "\r\n - соразмерного уменьшения установленной Цены Работ. " +
                    "\r\n5.2.При одностороннем отказе Заказчика от исполнения договора Исполнитель удерживает из суммы полученной предоплаты часть установленной цены пропорционально части Работ, выполненных до получения извещения об отказе Заказчика от исполнения Договора.  " +
                    "\r\n5.3.За нарушение сроков оплаты Работ, установленных Договором, Исполнитель вправе потребовать от Заказчика уплаты неустойки в виде пени за каждый день просрочки в размере 0,1% от Цены Работ, но не более 10 (Десяти) процентов от Цены Работ. " +
                    "\r\n5.4.За нарушение установленных Договором сроков выполнения Работ и поставки изготовленной продукции (результата Работ) Исполнитель уплачивает Заказчику неустойку в виде штрафа за каждый день просрочки в размере 0,1% от Цены Работ, но не более 10 (Десяти) процентов от Цены Работ. При осуществлении окончательного расчета за выполненные по Договору Работы часть Цены Работ, подлежащая уплате Заказчиком по факту сдачи-приемки выполненных Работ, уменьшается на сумму начисленной в порядке, предусмотренном настоящим пунктом неустойки." +
                    "\r\n5.5.Исполнитель несет риск случайной гибели или повреждения изготовленной продукции (результата Работ) до ее (его) приемки Заказчиком в установленном Договором порядке. " +
                    "\r\n5.6.Любые споры, которые могут возникнуть между Сторонами по настоящему Договору разрешаются путем переговоров. При невозможности достижения урегулирования спора путем переговоров, спор передается на разрешение в Арбитражный суд г. Москвы. \r\n");

                await Task.Run(() => SixParagraph = "6.1. Во всем ином, не урегулированном в настоящем Договоре, применяются нормы действующего гражданского законодательства." +
                    "\r\n6.2. Настоящий Договор составлен в двух экземплярах – по одному для каждой из Сторон. \r\n");

                await Task.Run(() => SevenParagraph = "Приложение 1 к Договору на изготовление и поставку персонализированной деловой и канцелярской продукции от " + Projects[0].ProjectDataStart);

                await Task.Run(() => GridInfo.Add(new DataGridInfo()
                {
                    ProductName = Projects[0].ProjectTurbineNavigation.TurbineName,
                    Unit = "шт.",
                    ProductCount = Projects[0].ProjectCount,
                    ProductCost = Math.Round(Convert.ToDouble(Projects[0].ProjectTurbineNavigation.TurbineCost) * Projects[0].ProjectCount, 2, MidpointRounding.AwayFromZero),
                    NDS = Math.Round(Convert.ToDouble(Projects[0].ProjectTurbineNavigation.TurbineCost * Projects[0].ProjectCount) * 0.1, 2, MidpointRounding.AwayFromZero),
                    ProductCostNDS = Math.Round(Convert.ToDouble(Projects[0].ProjectTurbineNavigation.TurbineCost * Projects[0].ProjectCount)
                        + (Convert.ToDouble(Projects[0].ProjectTurbineNavigation.TurbineCost * Projects[0].ProjectCount) * 0.1), 2, MidpointRounding.AwayFromZero)
                })); ;
            }
            catch
            {
                FailedAddOrUpdate = "*Не удалось загрузить данные. Откройте проект заново";
                ForegroundFailedMessage = -1;
            }

        }

        private string failedAddOrUpdate;
        public string FailedAddOrUpdate 
        { 
            get => failedAddOrUpdate; 
            set => Set(ref failedAddOrUpdate, value); 
        }

        private decimal foregroundFailedMessage;

        public decimal ForegroundFailedMessage 
        {
            get => foregroundFailedMessage; 
            set => Set(ref foregroundFailedMessage, value); 
        }

    }
}
