using System;
using System.Collections.Generic;
using System.Text;

delegate viod CharEventHandler(object sender, CharEventArgs e);

//委托的用法
public delegate void aaa(DateTime now); 
 public class 委托的两种使用方法
 {
     //方法1
     public event aaa CharEventArgs;
     public void function()
     {
         CharEventArgs(new char { 1}); //OnTimeChange事件触发   
     }
     //方法2
      public void function2()
     {
         this.BeginInvoke(new aaa(CharEventArgs),new char {1} );//计算完成需要在一个文本框里显示
     }
    
}
    



//自定义了一个触发事件的参数
public class CharEventArgs : EventArgs
{
    public char CurrChar;
    public CharEventArgs(char CurrChar)
    {
        this.CurrChar = CurrChar;
    }
}



class CharChecker
{
    public event CharEventHandler CharTest;
    public char curr_char;
    public char Curr_Char
    {
        get { return curr_char; }
        set
        {
            if (CharTest != null)
            {
                CharEventArgs myeven = new CharEventArgs(value);
                CharTest(this, myeven);// 在这里触发事件
                curr_char = myeven.CurrChar;
            }
        }
    }
}



class AppEvent
{
    static void Main()
    {
        CharChecker chartester = new CharChecker();
        chartester.CharTest += new CharEventHandler(Change_X);//触发事件Chartest时, 绑定到Change_X函数上
        chartester.Curr_Char = 'a'; //运行Curr_Char的Set方法, 触发事件

        Console.WriteLine("事件处理结果：{0}", chartester.Curr_Char);
        chartester.Curr_Char = 'b';

        Console.WriteLine("事件处理结果：{0}", chartester.Curr_Char);
        chartester.Curr_Char = 'x';

        Console.WriteLine("{0}", chartester.Curr_Char);
        Console.WriteLine();

    }

    static void Change_X(Object source, CharEventArgs e)
    {
        if (e.CurrChar == 'x' || e.CurrChar = 'X')
        {
            Console.Write("触发的字符是x,");
            Console.Write("把x替为：");
            e.CurrChar = '?';
        }
        else
            Console.Write("触发的字符不是x,");
    }
}
