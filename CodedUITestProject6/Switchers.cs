using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;

namespace CodedUITestProject6
{
    public class Ui :XamlWindow
    {
        public UiCalc UiCalc
        {
            get
            {
                if ((this.mUiCalc == null))
                {
                    this.mUiCalc = new UiCalc();
                }
                return this.mUiCalc;
            }
        }
        private UiCalc mUiCalc;
    }
    public class UiCalc : XamlWindow
    {
        public UiCalc()
        {
            this.SearchProperties[XamlControl.PropertyNames.Name] = "Калькулятор";
            this.SearchProperties[XamlControl.PropertyNames.ClassName] = "Windows.UI.Core.CoreWindow";
            this.WindowTitles.Add("Калькулятор");
        }
        public UiCalcText UiCalcText
        {
            get
            {
                if ((this.mUiCalcText == null))
                {
                    this.mUiCalcText = new UiCalcText(this);
                }
                return this.mUiCalcText;
            }
        }
        private UiCalcText mUiCalcText;
        public UiCalcNum UiCalcNum
        {
            get
            {
                if ((this.mUiCalcNum == null))
                {
                    this.mUiCalcNum = new UiCalcNum(this);
                }
                return this.mUiCalcNum;
            }
        }
        private UiCalcNum mUiCalcNum;
        public UiCalcOper UiCalcOper
        {
            get
            {
                if ((this.mUiCalcOpert == null))
                {
                    this.mUiCalcOpert = new UiCalcOper(this);
                }
                return this.mUiCalcOpert;
            }
        }
        private UiCalcOper mUiCalcOpert;
    }
    //*******************************************Окно результатов**********************************
    public class UiCalcText : XamlControl
    {
        public UiCalcText(XamlControl searchLimitContainer) :
        base(searchLimitContainer)
        {
            this.SearchProperties[UITestControl.PropertyNames.ControlType] = "Group";
            this.WindowTitles.Add("Калькулятор");
        }
        public XamlText UiText
        {
            get
            {
                if ((this.mUiText == null))
                {
                    this.mUiText = new XamlText(this);
                    this.mUiText.SearchProperties[XamlText.PropertyNames.AutomationId] = "CalculatorResults";
                    this.mUiText.WindowTitles.Add("Калькулятор");
                }
                return this.mUiText;
            }
        }

        private XamlText mUiText;
    }
    //*****************************************Кнопки операций***********************************
    public class UiCalcOper : XamlControl
    {
        public UiCalcOper(XamlControl searchLimitContainer) :
        base(searchLimitContainer)
        {
            this.SearchProperties[UITestControl.PropertyNames.ControlType] = "Group";
            this.SearchProperties["AutomationId"] = "StandardOperators";
            this.WindowTitles.Add("Калькулятор");
        }
        public string TypeOfOper
        {
            get
            {
                return _typeOfOper;
            }
            set
            {
                switch (value)//преобразование операций в названия кнопок
                {
                    case "плюс":
                        _typeOfOper = "plusButton";
                        break;
                    case "минус":
                        _typeOfOper = "minusButton";
                        break;
                    case "умножить":
                        _typeOfOper = "multiplyButton";
                        break;
                    case "разделить":
                        _typeOfOper = "divideButton";
                        break;
                    case "равно":
                        _typeOfOper = "equalButton";
                        break;
                    case "знак"://на вырост - в программе не используется
                        _typeOfOper = "negateButton";
                        break;
                    default:
                        _typeOfOper = "plusButton";
                        break;
                }
            }

        }
        private string _typeOfOper;//здесь храним операцию

        public XamlButton OperationButton
        {
            get
            {
                this.mOperationButton = new XamlButton(this);
                this.mOperationButton.SearchProperties[XamlButton.PropertyNames.AutomationId] = _typeOfOper;
                this.mOperationButton.WindowTitles.Add("Калькулятор");
                return mOperationButton;
            }
        }
        private XamlButton mOperationButton;
    }
  
    //*******************************************Кнопки цифр**************************************
    public class UiCalcNum : XamlControl
    {
        public UiCalcNum(XamlControl searchLimitContainer) :
        base(searchLimitContainer)
        {
            #region Условия поиска
            this.SearchProperties[UITestControl.PropertyNames.ControlType] = "Group";
            this.SearchProperties["AutomationId"] = "NumberPad";
            this.WindowTitles.Add("Калькулятор");
            #endregion
        }

        public string TypeOfButton { get => _typeOfButton; set =>  _typeOfButton = "num" + value + "Button"; }
        private string _typeOfButton;//здесь имя кнопки с цифрой которую нажимаем
        public XamlButton NumberButton
        {
            get
            {
                this.mNumberButton = new XamlButton(this);
                this.mNumberButton.SearchProperties[XamlButton.PropertyNames.AutomationId] = _typeOfButton;
                this.mNumberButton.WindowTitles.Add("Калькулятор");
                return mNumberButton;
            }
        }
        private XamlButton mNumberButton;

    }

}

