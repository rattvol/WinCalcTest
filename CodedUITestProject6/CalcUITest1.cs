using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CodedUITestProject6
{
    [Serializable]
    public class Test
    {
        [XmlElement]
        public int firstNum { get; set; }
        [XmlElement]

        public int secondNum { get; set; }
        [XmlElement]
        public string operation { get; set; }
        [XmlElement]
        public string result { get; set; }
    }

    [CodedUITest(CodedUITestType.WindowsStore)]
    public class CalcUITest1
    {
        //===========Путь к размещению файлов с входным данными и результатами======================
        public string path = "C:/Users/User/source/repos/CodedUITestProject6/CodedUITestProject6/";
        //==========================================================================================

        [TestInitialize]
        public void Start()
        {
            //Запуск калькулятора
            XamlWindow.Launch("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
        }
        [TestMethod]
        public void CodedUITestMethod2()
        {

            //чтение XML-файла с входными данными
            StreamReader stream = new StreamReader(path + "Data.xml");
            XmlRootAttribute xroot = new XmlRootAttribute();
            xroot.ElementName = "TestData";
            xroot.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Test>), xroot);
            List<Test> testData = (List<Test>)serializer.Deserialize(stream);
            //Выполнение тестов
            for (int l = 0; l < testData.Count; l++)
            {

                //Ввод первого числа
                string num = Convert.ToString(testData[l].firstNum);
                for (int i = 0; i < num.Length; i++)
                {
                    this.Ui.UiCalc.UiCalcNum.TypeOfButton = num.Substring(i, 1);
                    Gesture.Tap(this.Ui.UiCalc.UiCalcNum.NumberButton);
                }
                //Ввод типа операции
                Ui.UiCalc.UiCalcOper.TypeOfOper = testData[l].operation;
                Gesture.Tap(this.Ui.UiCalc.UiCalcOper.OperationButton);


                //Ввод второго числа
                num = Convert.ToString(testData[l].secondNum);
                for (int i = 0; i < num.Length; i++)
                {
                    Ui.UiCalc.UiCalcNum.TypeOfButton = num.Substring(i, 1);
                    Gesture.Tap(this.Ui.UiCalc.UiCalcNum.NumberButton);
                }
                //Равно
                Ui.UiCalc.UiCalcOper.TypeOfOper = "равно";
                Gesture.Tap(this.Ui.UiCalc.UiCalcOper.OperationButton);
                //Сравнение
                string result = this.Ui.UiCalc.UiCalcText.UiText.DisplayText.Substring(19);
                string resYes;
                switch (testData[l].result == result)
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

                //Вывод результатов
                try
                {
                    Assert.AreEqual(result, testData[l].result);
                }
                catch
                {
                    //результаты в файле result.csv
                }
               
                //Вывод в CSV файл
                //---Coздание файла если он несуществует
                FileInfo fileInf = new FileInfo(path + "result.csv");
                if (!fileInf.Exists)
                {
                    FileStream fs = fileInf.Create();
                    fs.Close();
                    using (StreamWriter sw = new StreamWriter(path + "result.csv", true, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine("Дата и время;Первое число;Операция;Второе число;Ожидаемый результат;Фактический результат;Результат теста");
                    }

                }
                using (StreamWriter sw = new StreamWriter(path + "result.csv", true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine("{0};{1};{2};{3};{4};{5};{6}", DateTime.Now, testData[l].firstNum, testData[l].operation, testData[l].secondNum, testData[l].result, result, resYes); ; ;
                }
            }
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
    }
}
