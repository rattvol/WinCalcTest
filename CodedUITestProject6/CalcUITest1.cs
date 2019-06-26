using System;
//using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.IO;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using System.Diagnostics;

namespace CodedUITestProject6
{

    //[CodedUITest(CodedUITestType.WindowsStore)]
    [CodedUITest]
    public class CalcUITest1
    {
        //===========Путь к размещению файла с результатом======================
        public string path = "C:/Users/User/source/repos/CodedUITestProject6/CodedUITestProject6/result.csv";
        //==========================================================================================
        [TestInitialize]
        public void Start()
        {
            //Запуск калькулятора

            XamlWindow.Launch("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
           
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "operation", DataAccessMethod.Sequential), DeploymentItem("Data.xml")]
        [TestMethod]
        public void CodedUITestMethod2()
        {
            //***********************************Выполнение тестов
            //Ввод первого числа
                string firstNum = TestContext.DataRow["firstNum"].ToString();

            for (int i = 0; i < firstNum.Length; i++)
            {
                this.Ui.UiCalc.UiCalcNum.TypeOfButton = firstNum.Substring(i, 1);
                //Mouse.Click(this.);
                Gesture.Tap(this.Ui.UiCalc.UiCalcNum.NumberButton);
            }
            //Ввод типа операции
            string operation = TestContext.DataRow["function"].ToString();
                Ui.UiCalc.UiCalcOper.TypeOfOper = operation;
                Gesture.Tap(this.Ui.UiCalc.UiCalcOper.OperationButton);


            //Ввод второго числа
            string secondNum = TestContext.DataRow["secondNum"].ToString();
            for (int i = 0; i < secondNum.Length; i++)
                {
                    Ui.UiCalc.UiCalcNum.TypeOfButton = secondNum.Substring(i, 1);
                    Gesture.Tap(this.Ui.UiCalc.UiCalcNum.NumberButton);
                }
                //Равно
                Ui.UiCalc.UiCalcOper.TypeOfOper = "равно";
                Gesture.Tap(this.Ui.UiCalc.UiCalcOper.OperationButton);
                //Сравнение
                string result = this.Ui.UiCalc.UiCalcText.UiText.DisplayText.Substring(19);
                string resYes;
                switch (TestContext.DataRow["result"].ToString() == result)
                {
                    case true:
                        resYes = "Пройден";
                        break;
                    case false:
                        resYes = "Не пройден";
                        break;
                    default:
                        resYes = "Не известно";
                        break;
                }

                //Вывод в CSV файл
                //---Coздание файла если он несуществует
                FileInfo fileInf = new FileInfo(path);
                if (!fileInf.Exists)
                {
                    FileStream fs = fileInf.Create();
                    fs.Close();
                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine("Дата и время;Первое число;Операция;Второе число;Ожидаемый результат;Фактический результат;Результат теста");
                    }

                }
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6}", DateTime.Now, firstNum, operation, secondNum, TestContext.DataRow["result"].ToString(), result, resYes);
                }
                                Assert.AreEqual(result, TestContext.DataRow["result"].ToString());

        }
        //***********************************************************************
        [TestCleanup]
        public void Finish()
        {

        }
        public Ui Ui
        {
            get
            {
                if (this.mUi == null)
                {
                    this.mUi = new Ui();
                }

                return this.mUi;
            }
        }
        private Ui mUi;
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
    }
}
