﻿using MathNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Function Root;
        Function Active;
        Function Last;
        public MainWindow()
        {
            InitializeComponent();
            Root = new ConstNum(null);
            Active = Root;
            ReloadScreen();
        }
        /// <summary>
        ///     Funkce se zavolá po stisku tlačítka s číslem, po zavolání přidá na aktuální číslo číslici z tlačítka, pokud není číslo moc velké
        /// </summary>
        /// <param name="sender">stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void NumKeyPress(object sender, RoutedEventArgs e)
        {
            double value = 0;
            if (double.TryParse(((Button)sender).Content.ToString(), out value))
            {
                if (Active.GetType() == typeof(ConstNum))
                {
                    if (((ConstNum)Active).point && ((ConstNum)Active).ToString().Remove(((ConstNum)Active).ToString().IndexOf(',')).Length + ((ConstNum)Active).pointPos < 15)
                    {
                        string text = ((ConstNum)Active).ToString();
                        text += value;
                        ((ConstNum)Active).SetValue(double.Parse(text));
                        ((ConstNum)Active).pointPos++;
                    }
                    else if (!((ConstNum)Active).point)
                    {
                        value += ((ConstNum)Active).GetValue() * 10;
                        ((ConstNum)Active).SetValue(value);
                    }
                }
                else
                {
                    Active.setRightFunction(new ConstNum(value, Active));
                    Active = Active.getRightFunction();
                }
                ReloadScreen();
            }
        }
        /// <summary>
        ///     Po zavolání se vypíše příklad na display
        /// </summary>
        private void ReloadScreen()
        {
            Display_equation.Content = Root.ToString();
        }
        /// <summary>
        ///     pokud aktuální číslo nemá desetinnou čárku, tak jí tato funkce přidá
        /// </summary>
        /// <param name="sender">Stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void pointButtonClick(object sender, RoutedEventArgs e)
        {
            if (Active.GetType() == typeof(ConstNum) && !((ConstNum)Active).point)
            {
                ((ConstNum)Active).point = true;
                ReloadScreen();
            }
        }
        /// <summary>
        ///     Po zavolání přidá za aktuální číslo funkci z tlačítka a vytvoří a aktivuje nové číslo za funkcí
        /// </summary>
        /// <param name="sender">stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void FunctionButtonClick(object sender, RoutedEventArgs e)
        {
            if (Active.type == Function.FuncType.Constant || Active.type == Function.FuncType.Unary)
            {
                Last = Active;
                if (((Button)sender).Content.ToString() == "+" || ((Button)sender).Content.ToString() == "-")
                {
                    Function temp;
                    if (((Button)sender).Content.ToString() == "+")
                    {
                        temp = new AddFunc(Root, null);
                    }
                    else
                    {
                        temp = new SubFunc(Root, null);
                    }
                    Root.parentFunction = temp;
                    temp.setRightFunction(new ConstNum(temp));
                    Last = Active;
                    Active = temp.rightFunction;
                    Root = temp;

                }
                else if (((Button)sender).Content.ToString() == "×" || ((Button)sender).Content.ToString() == "÷" || ((Button)sender).Content.ToString() == "%")
                {
                    Function temp;
                    if (Active.parentFunction != null && (Active.parentFunction.name == Function.FuncName.Mul || Active.parentFunction.name == Function.FuncName.Pow))
                    {
                        if (((Button)sender).Content.ToString() == "×")
                        {
                            temp = new MulFunc(Active.parentFunction, Active.parentFunction.parentFunction);
                        }
                        else if(((Button)sender).Content.ToString() == "%")
                        {
                            temp = new ModFunc(Active.parentFunction, Active.parentFunction.parentFunction);
                        }
                        else
                        {
                            temp = new DivFunc(Active.parentFunction, Active.parentFunction.parentFunction);
                        }

                        if (Active.parentFunction.parentFunction == null)
                            Root = temp;
                        else Active.parentFunction.parentFunction.setRightFunction(temp);

                        Active.parentFunction = temp;
                    }
                    else
                    {
                        if (((Button)sender).Content.ToString() == "×")
                        {
                            temp = new MulFunc(Active, Active.parentFunction);
                        }
                        else if (((Button)sender).Content.ToString() == "%")
                        {
                            temp = new ModFunc(Active, Active.parentFunction);
                        }
                        else
                        {
                            temp = new DivFunc(Active, Active.parentFunction);
                        }

                        if (Active.parentFunction != null)
                            Active.parentFunction.setRightFunction(temp);
                        else
                            Root = temp;

                    }
                    temp.setRightFunction(new ConstNum(temp));
                    Active = temp.rightFunction;
                }
                else if (((Button)sender).Content.ToString() == "y√x" || ((Button)sender).Content.ToString() == "x^y")
                {
                    Function temp;
                    if (((Button)sender).Content.ToString() == "y√x")
                    {
                        temp = new SqrtFunc(Active, Active.parentFunction);
                    }
                    else
                    {
                        temp = new PowFunc(Active, Active.parentFunction);
                    }
                    if (Active.parentFunction != null)
                        Active.parentFunction.setRightFunction(temp);
                    else
                        Root = temp;

                    temp.setRightFunction(new ConstNum(temp));
                    Active = temp.rightFunction;
                }
                else if (((Button)sender).Content.ToString() == "!x" && Active.type == Function.FuncType.Constant)
                {
                    Function temp = new FactorialFunc(CalcMath.Abs(Math.Round(((ConstNum)Active).GetValue())), Active.parentFunction);
                    if (Active.parentFunction != null && Active.parentFunction.leftFunction == Active)
                    {
                        Active.parentFunction.setLeftFunction(temp);
                    }
                    else if (Active.parentFunction != null && Active.parentFunction.rightFunction == Active)
                    {
                        Active.parentFunction.setRightFunction(temp);
                    }
                    else
                    {
                        Root = temp;
                    }
                    Active = temp;
                }
            }
            ReloadScreen();
        }
        /// <summary>
        ///     Po stisknutí příslušného tlačítka odstraní poslední číslici aktuálního čísla, pokud je číslo při zavolání 0, odstraní poslední funkci
        /// </summary>
        /// <param name="sender">stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void DELButtonClick(object sender, RoutedEventArgs e)
        {
            if (Active.GetType() == typeof(ConstNum))
            {
                if (((ConstNum)Active).GetValue() != 0 || ((ConstNum)Active).point)
                {
                    string value = ((ConstNum)Active).ToString();
                    if (((ConstNum)Active).point && ((ConstNum)Active).pointPos == 0)
                    {
                        if (value.IndexOf(',') > 0)
                        {
                            value = value.Remove(value.IndexOf(','));
                            ((ConstNum)Active).SetValue(double.Parse(value));
                        }

                        ((ConstNum)Active).point = false;
                        ((ConstNum)Active).pointPos = 0;
                    }
                    else
                    {
                        if (value.Length > 1)
                        {
                            if (value.Length == 2 && value[0] == '-')
                            {
                                ((ConstNum)Active).SetValue(0);
                            }
                            else
                            {
                                value = value.Remove(value.Length - 1);
                                if (((ConstNum)Active).point)
                                    ((ConstNum)Active).pointPos--;
                                ((ConstNum)Active).SetValue(double.Parse(value));
                            }
                        }
                        else
                        {
                            ((ConstNum)Active).SetValue(0);
                        }
                    }
                }
                else if (((ConstNum)Active).GetValue() == 0 && ((ConstNum)Active) != Root)
                {
                    Active = Active.parentFunction;
                    if(Active.parentFunction==null)
                    {
                        Active = Active.leftFunction;
                        Active.parentFunction = null;
                        Root = Active;
                        if(Active.type!=Function.FuncType.Constant && Active.type != Function.FuncType.Unary)
                            Active = Active.rightFunction;
                    }
                    else
                    {
                        Active.parentFunction.setRightFunction(Active.leftFunction);
                        Active.leftFunction.parentFunction = Active.parentFunction;
                        Active = Active.leftFunction;
                        while (Active.type != Function.FuncType.Constant && Active.type != Function.FuncType.Unary)
                        {
                            Active = Active.rightFunction;
                        }
                    }
                }
            }
            ReloadScreen();
        }
        /// <summary>
        ///     Odstraní celý příklad, vytvoří nový a nechá přepsat display
        /// </summary>
        /// <param name="sender">stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            Root = new ConstNum(null);
            Active = Root;
            Display_result.Content = "";
            ReloadScreen();
        }
        /// <summary>
        ///     Vyžádá si od kořenové funkce hodnotu, která se získá stromově ze všech ostatních funkcí a výslednou hodnotu vypíše na display
        /// </summary>
        /// <param name="sender">stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void SolveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Display_result.Content = Root.Solve();
            }
            catch
            {
                Display_result.Content = "Syntax Error!";
            }
        }
        /// <summary>
        ///     Neguje hodnotu aktuálního čísla, pokud číslo náleží funkci + nebo -, tak neneeguje číslo ale samotnou funkci
        /// </summary>
        /// <param name="sender">stisknuté tlačítko</param>
        /// <param name="e"></param>
        private void InvertButtonClick(object sender, RoutedEventArgs e)
        {
            if (Active.GetType() == typeof(ConstNum))
            {
                if (Active.parentFunction != null && Active.parentFunction.name == Function.FuncName.Add)
                {
                    SubFunc temp = new SubFunc(Active.parentFunction.leftFunction, Active.parentFunction.parentFunction);
                    temp.setRightFunction(Active);
                    if (Active.parentFunction.parentFunction != null)
                        Active.parentFunction.parentFunction.setRightFunction(temp);
                    else
                        Root = temp;
                    Active.parentFunction = temp;
                }
                else if (Active.parentFunction != null && Active.parentFunction.name == Function.FuncName.Sub)
                {
                    AddFunc temp = new AddFunc(Active.parentFunction.leftFunction, Active.parentFunction.parentFunction);
                    temp.setRightFunction(Active);
                    if (Active.parentFunction.parentFunction != null)
                        Active.parentFunction.parentFunction.setRightFunction(temp);
                    else
                        Root = temp;
                    Active.parentFunction = temp;
                }
                else
                {
                    ((ConstNum)Active).SetValue(((ConstNum)Active).GetValue() * -1);
                }
            }
            ReloadScreen();
        }
    }


}
