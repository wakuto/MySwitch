using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program {
    static void Main(string[] args) {
        (string, string) gengo = ("平成", "令和");
        int gengoIdx = new MySwitch<(string, string), int>(gengo)
                       .Case(("令和", "平成"), 0)
                       .Case(("昭和", "平成"), 1)
                       .Case(("平成", "令和"), 2)
                       .Default(-1);
        Console.WriteLine($"{gengo}のインデックスは{gengoIdx}です。");
        Console.ReadLine();
    }
}

/// <summary>
/// switch式が使えない環境で同じような機能を提供します。最後に必ずDefaultを挿入する必要があります。
/// </summary>
/// <typeparam name="U">比較対象の型</typeparam>
/// <typeparam name="T">式の返り値の型</typeparam>
class MySwitch<U, T> {
    // T:switchで返す値の型
    // U:switchで比較する型
    T res;
    dynamic cmp = default(U);
    private bool match = false;

    public MySwitch(U obj) {
        this.cmp = obj;
    }

    /// <summary>
    /// case句
    /// </summary>
    /// <param name="targ">比較のラベル</param>
    /// <param name="num">マッチした場合の値</param>
    /// <returns></returns>
    public MySwitch<U, T> Case(U targ, T num) {
        if(targ.Equals(cmp)) {
            match = true;
            res = num;
        }
        return this;
    }
    /// <summary>
    /// default句
    /// </summary>
    /// <param name="num">デフォルトの値</param>
    /// <returns></returns>
    public T Default(T num) {
        if (match) {
            return res;
        } else {
            return num;
        }
    }
}
