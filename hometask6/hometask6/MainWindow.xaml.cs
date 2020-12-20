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

namespace hometask6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public abstract class Figure
    {
        public struct point
        {
            public double x;
            public double y;
        }
        public virtual double Perimeter()
        {
            return 0;
        }
        public virtual double Area()
        {
            return 0;
        }
        public override bool Equals(object figure)
        {
            return false;
        }
        public override string ToString()
        {
            return "Некая абстрактная фигура";
        }
    }

    public class Circle : Figure
        //круг
    {
        Figure.point O;
        double R;
        public override double Perimeter()
            // периметр
        {
            return 2*Math.PI*R;
        }
        public override double Area()
            // площадь
        {
            return Math.PI*R*R;
        }
        double GetR()
        {
            return R;
        }
        public override bool Equals(object figure)
            //сравнение
        {
            if (figure is Circle)
            {
                if ((figure as Circle).GetR() == R) return true;
                else return false;
            }
            else return false;
        }
        public override string ToString()
        {
            return $"круг радиуса {R} с центром в O({O.x}; {O.y})";
        }
        public Circle(double xo, double yo, double r)
        {
            if (r > 0)
            {
                O.x = xo;
                O.y = yo;
                R = r;
            }
            else throw new Exception("Такого круга не существует");
        }
    }

    public class Rectangle : Figure
        // прямоугольник
    {
        Figure.point A;
        Figure.point B;
        Figure.point C;
        Figure.point D;

        int ParallelSides()
        //какие пары сторон параллельны
        {
            if ((B.x-A.x)*(D.x-A.x)+(B.y-A.y)*(D.y-A.y)==0) return 1;
            else if ((B.x - A.x) * (C.x - A.x) + (B.y - A.y) * (C.y - A.y) == 0) return 2;
            else if ((C.x - A.x) * (D.x - A.x) + (C.y - A.y) * (D.y - A.y) == 0) return 3;
            else return 0;
        }

        public override double Perimeter()
            //периметр
        {
            double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
            double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            double ad = Math.Sqrt(Math.Pow(A.x - D.x, 2) + Math.Pow(A.y - D.y, 2));
            double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
            double bd = Math.Sqrt(Math.Pow(D.x - B.x, 2) + Math.Pow(D.y - B.y, 2));
            double cd = Math.Sqrt(Math.Pow(C.x - D.x, 2) + Math.Pow(C.y - D.y, 2));
            switch (this.ParallelSides())
            {
                case 1:
                    return ab + bc + cd + ad;
                    break;
                case 2:
                    return ac + cd + bd + ab;
                    break;
                case 3:
                    return ac + bc + bd + ad;
                    break;
                default:
                    return base.Perimeter();
                    break;
            }
            
        }
        public override double Area()
            // площадь
        {
            double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
            double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            double ad = Math.Sqrt(Math.Pow(A.x - D.x, 2) + Math.Pow(A.y - D.y, 2));
            double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
            double bd = Math.Sqrt(Math.Pow(D.x - B.x, 2) + Math.Pow(D.y - B.y, 2));
            double cd = Math.Sqrt(Math.Pow(C.x - D.x, 2) + Math.Pow(C.y - D.y, 2));
            switch (this.ParallelSides())
            {
                case 1:
                    return ab * bc;
                    break;
                case 2:
                    return ab * ac;
                    break;
                case 3:
                    return ac * bc;
                    break;
                default:
                    return base.Area();
                    break;
            }
        }
        public override bool Equals(object figure)
            // сравнение
        {
            if (figure is Rectangle)
            {
                // прямоугольники равны если их периметры и площади совпадают
                if (this.Perimeter() == (figure as Rectangle).Perimeter() && this.Area() == (figure as Rectangle).Area()) return true;
                else return false;
            }
            else return false;
        }

        public override string ToString()
        {
            return $"прямоугольник с вершинами в точках A({A.x}; {A.y}) B({B.x}; {B.y}) C({C.x}; {C.y}) D({D.x}; {D.y})";
        }

        public Rectangle(double xa, double ya, double xb, double yb, double xc, double yc , double xd, double yd)
        {
            double ab = Math.Sqrt(Math.Pow(xa - xb, 2) + Math.Pow(ya - yb, 2));
            double ac = Math.Sqrt(Math.Pow(xa - xc, 2) + Math.Pow(ya - yc, 2));
            double ad = Math.Sqrt(Math.Pow(xa - xd, 2) + Math.Pow(ya - yd, 2));
            double bc = Math.Sqrt(Math.Pow(xc - xb, 2) + Math.Pow(yc - yb, 2));
            double bd = Math.Sqrt(Math.Pow(xd - xb, 2) + Math.Pow(yd - yb, 2));
            double cd = Math.Sqrt(Math.Pow(xc - xd, 2) + Math.Pow(yc - yd, 2));
            // прямой угол проверяется через скалярное произведение
            if (ab != 0 && ac != 0 && ad != 0 && bc != 0 && bd != 0 && cd != 0 &&
                ((ab == cd && bc == ad && (xb - xa) * (xd - xa) + (yb - ya) * (yd - ya) == 0) ||
                (ab == cd && ac == bd && (xc - xa) * (xd - xa) + (yc - ya) * (yd - ya) == 0) ||
                (ac == bd && bc == ad && (xb - xa) * (xc - xa) + (yb - ya) * (yc - ya) == 0))) 
            {
                A.x = xa;
                A.y = ya;
                B.x = xb;
                B.y = yb;
                C.x = xc;
                C.y = yc;
                D.x = xd;
                D.y = yd;
            }
            else throw new Exception("Такого прямоугольника не существует");
        }
    }

    public class Triangle : Figure
        // треугольник, вспомогательный
    {
        Figure.point A;
        Figure.point B;
        Figure.point C;

        double GetA()
        {
            return Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
        }
        double GetB()
        {
            return Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
        }
        double GetC()
        {
            return Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
        }
        public override double Perimeter()
        {
            double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
            double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
            return ab + bc + ac;
        }
        public override double Area()
        {
            double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
            double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
            double p = this.Perimeter() / 2;
            return Math.Sqrt(p * (p - ab) * (p - ac) * (p - bc));
        }
        public override bool Equals(object figure)
        {
            if (figure is Triangle)
            {
                double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
                double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
                double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
                double newa = (figure as Triangle).GetA();
                double newb = (figure as Triangle).GetB();
                double newc = (figure as Triangle).GetC();
                if (ab == newa && ac == newb && bc == newc ||
                    ab == newa && ac == newc && bc == newb ||
                    ab == newb && ac == newa && bc == newc ||
                    ab == newb && ac == newc && bc == newa ||
                    ab == newc && ac == newa && bc == newb ||
                    ab == newc && ac == newb && bc == newa) return true;
                else return false;
            }
            else return false;
        }
        public Triangle(double xa, double ya, double xb, double yb, double xc, double yc)
        {
            A.x = xa;
            A.y = ya;
            B.x = xb;
            B.y = yb;
            C.x = xc;
            C.y = yc;
        }
    }

    public class Trapeze : Figure
        // трапеция
    {
        Figure.point A;
        Figure.point B;
        Figure.point C;
        Figure.point D;

        Figure.point GetA() { return A; }
        Figure.point GetB() { return B; }
        Figure.point GetC() { return C; }
        Figure.point GetD() { return D; }
        int TypeOFTrapeze()
            // определение какие стороны трапеции параллельны
        {
            Figure.point AB;
            Figure.point AC;
            Figure.point AD;
            Figure.point BC;
            Figure.point BD;
            Figure.point CD;
            AB.x = B.x - A.x;
            AB.y = B.y - A.y;
            AC.x = C.x - A.x;
            AC.y = C.y - A.y;
            AD.x = D.x - A.x;
            AD.y = D.y - A.y;
            BC.x = C.x - B.x;
            BC.y = C.y - B.y;
            BD.x = D.x - B.x;
            BD.y = D.y - B.y;
            CD.x = D.x - C.x;
            CD.y = D.y - C.y;

            // длины сторон
            double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
            double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            double ad = Math.Sqrt(Math.Pow(A.x - D.x, 2) + Math.Pow(A.y - D.y, 2));
            double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
            double bd = Math.Sqrt(Math.Pow(D.x - B.x, 2) + Math.Pow(D.y - B.y, 2));
            double cd = Math.Sqrt(Math.Pow(C.x - D.x, 2) + Math.Pow(C.y - D.y, 2));

            // углы
            double abc = Math.Acos((AB.x * -BC.x + AB.y * -BC.y) / (ab * bc));
            double bcd = Math.Acos((BC.x * -CD.x + BC.y * -CD.y) / (bc * cd));
            double cda = Math.Acos((CD.x * AD.x + CD.y * AD.y) / (cd * ad));
            double dab = Math.Acos((AB.x * AD.x + AB.y * AD.y) / (ab * ad));
            double acd = Math.Acos((AC.x * -CD.x + AC.y * -CD.y) / (ac * cd));
            double cdb = Math.Acos((CD.x * BD.x + CD.y * BD.y) / (cd * bd));
            double dba = Math.Acos((AB.x * -BD.x + AB.y * -BD.y) / (ab * bd));
            double bac = Math.Acos((AB.x * AC.x + AB.y * AC.y) / (ab * ac));
            double acb = Math.Acos((AC.x * BC.x + AC.y * BC.y) / (ac * bc));
            double cbd = Math.Acos((BD.x * BC.x + BD.y * BC.y) / (bd * bc));
            double bda = Math.Acos((AD.x * BD.x + AD.y * BD.y) / (ad * bd));
            double dac = Math.Acos((AD.x * AC.x + AD.y * AC.y) / (ad * ac));

            if (AB.x * CD.y == AB.y * CD.x && abc + bcd + cda + dab == 2 * Math.PI) return 1;
            else if (BC.x * AD.y == BC.y * AD.x && abc + bcd + cda + dab == 2 * Math.PI) return 2;
            else if (AC.x * BD.y == AC.y * BD.x && acd + cdb + dba + bac == 2 * Math.PI) return 3;
            else if (AB.x * CD.y == AB.y * CD.x && acd + cdb + dba + bac == 2 * Math.PI) return 4;
            else if (AC.x * BD.y == AC.y * BD.x && acb + cbd + bda + dac == 2 * Math.PI) return 5;
            else if (AD.x * BC.y == AD.y * BC.x && acb + cbd + bda + dac == 2 * Math.PI) return 6;
            else return 0;
        }
        Figure.point CrossPoint()
            // точка пересечения продолжений боковых сторон
        {
            Figure.point O;
            double k1, k2, b1, b2;
            switch (this.TypeOFTrapeze())
            {
                case 1:
                    if (C.x != B.x && D.x != A.x)
                    {
                        k1 = (C.y - B.y) / (C.x - B.x);
                        b1 = B.y - B.x * k1;
                        k2 = (D.y - A.y) / (D.x - A.x);
                        b2 = A.y - A.x * k2;
                        O.x = (b2 - b1) / (k1 - k2);
                        O.y = k1 * O.x + b1;
                    }
                    else
                    {
                        k1 = (C.x - B.x) / (C.y - B.y);
                        b1 = B.x - B.y * k1;
                        k2 = (D.x - A.x) / (D.y - A.y);
                        b2 = A.x - A.y * k2;
                        O.y = (b2 - b1) / (k1 - k2);
                        O.x = k1 * O.y + b1;
                    }
                    break;
                case 2:
                    if (A.x != B.x && C.x != D.x)
                    {
                        k1 = (A.y - B.y) / (A.x - B.x);
                        b1 = B.y - B.x * k1;
                        k2 = (D.y - C.y) / (D.x - C.x);
                        b2 = C.y - C.x * k2;
                        O.x = (b2 - b1) / (k1 - k2);
                        O.y = k1 * O.x + b1;
                    }
                    else
                    {
                        k1 = (A.x - B.x) / (A.y - B.y);
                        b1 = B.x - B.y * k1;
                        k2 = (D.x - C.x) / (D.y - C.y);
                        b2 = C.x - C.y * k2;
                        O.y = (b2 - b1) / (k1 - k2);
                        O.x = k1 * O.y + b1;
                    }
                    break;
                case 3:
                    if (D.x != C.x && B.x != A.x)
                    {
                        k1 = (D.y - D.y) / (D.x - C.x);
                        b1 = C.y - C.x * k1;
                        k2 = (B.y - A.y) / (B.x - A.x);
                        b2 = A.y - A.x * k2;
                        O.x = (b2 - b1) / (k1 - k2);
                        O.y = k1 * O.x + b1;
                    }
                    else
                    {
                        k1 = (D.x - D.x) / (D.y - C.y);
                        b1 = C.x - C.y * k1;
                        k2 = (B.x - A.x) / (B.y - A.y);
                        b2 = A.x - A.y * k2;
                        O.y = (b2 - b1) / (k1 - k2);
                        O.x = k1 * O.y + b1;
                    }
                    break;
                case 4:
                    if (A.x != C.x && B.x != D.x)
                    {
                        k1 = (A.y - C.y) / (A.x - C.x);
                        b1 = C.y - C.x * k1;
                        k2 = (B.y - D.y) / (B.x - D.x);
                        b2 = D.y - D.x * k2;
                        O.x = (b2 - b1) / (k1 - k2);
                        O.y = k1 * O.x + b1;
                    }
                    else
                    {
                        k1 = (A.x - C.x) / (A.y - C.y);
                        b1 = C.x - C.y * k1;
                        k2 = (B.x - D.x) / (B.y - D.y);
                        b2 = D.x - D.y * k2;
                        O.y = (b2 - b1) / (k1 - k2);
                        O.x = k1 * O.y + b1;
                    }
                    break;
                case 5:
                    if (B.x != C.x && D.x != A.x)
                    {
                        k1 = (B.y - C.y) / (B.x - C.x);
                        b1 = C.y - C.x * k1;
                        k2 = (D.y - A.y) / (D.x - A.x);
                        b2 = A.y - A.x * k2;
                        O.x = (b2 - b1) / (k1 - k2);
                        O.y = k1 * O.x + b1;
                    }
                    else
                    {
                        k1 = (B.x - C.x) / (B.y - C.y);
                        b1 = C.x - C.y * k1;
                        k2 = (D.x - A.x) / (D.y - A.y);
                        b2 = A.x - A.y * k2;
                        O.y = (b2 - b1) / (k1 - k2);
                        O.x = k1 * O.y + b1;
                    }
                    break;
                case 6:
                    if (A.x != C.x && D.x != B.x)
                    {
                        k1 = (A.y - C.y) / (A.x - C.x);
                        b1 = C.y - C.x * k1;
                        k2 = (D.y - B.y) / (D.x - B.x);
                        b2 = B.y - B.x * k2;
                        O.x = (b2 - b1) / (k1 - k2);
                        O.y = k1 * O.x + b1;
                    }
                    else
                    {
                        k1 = (A.x - C.x) / (A.y - C.y);
                        b1 = C.x - C.y * k1;
                        k2 = (D.x - B.x) / (D.y - B.y);
                        b2 = B.x - B.y * k2;
                        O.y = (b2 - b1) / (k1 - k2);
                        O.x = k1 * O.y + b1;
                    }
                    break;
                default:
                    O.x = 0;
                    O.y = 0;
                    break;
            }
            return O;
        }

        public override double Perimeter()
            // периметр
        {
            double ab = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
            double ac = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            double ad = Math.Sqrt(Math.Pow(A.x - D.x, 2) + Math.Pow(A.y - D.y, 2));
            double bc = Math.Sqrt(Math.Pow(C.x - B.x, 2) + Math.Pow(C.y - B.y, 2));
            double bd = Math.Sqrt(Math.Pow(D.x - B.x, 2) + Math.Pow(D.y - B.y, 2));
            double cd = Math.Sqrt(Math.Pow(C.x - D.x, 2) + Math.Pow(C.y - D.y, 2));
            switch (this.TypeOFTrapeze())
            {
                case 1:
                    return ab + bc + cd + ad;
                    break;
                case 2:
                    return ab + bc + cd + ad;
                    break;
                case 3:
                    return ac + cd + bd + ab;
                    break;
                case 4:
                    return ac + cd + bd + ab;
                    break;
                case 5:
                    return ac + bc + bd + ad;
                    break;
                case 6:
                    return ac + bc + bd + ad;
                    break;
                default:
                    return base.Area();
                    break;
            }
        }

        public override double Area()
            // площадь
        {
            Figure.point O = this.CrossPoint();
            Figure triangle1, triangle2;
            double p, s1, s2;
            switch (this.TypeOFTrapeze())
            {
                case 1:
                    triangle1 = new Triangle(A.x, A.y, B.x, B.y, O.x, O.y);
                    triangle2 = new Triangle(C.x, C.y, D.x, D.y, O.x, O.y);
                    return Math.Abs(triangle1.Area() - triangle2.Area());
                    break;
                case 2:
                    triangle1 = new Triangle(B.x, B.y, C.x, C.y, O.x, O.y);
                    triangle2 = new Triangle(A.x, A.y, D.x, D.y, O.x, O.y);
                    return Math.Abs(triangle1.Area() - triangle2.Area());
                    break;
                case 3:
                    triangle1 = new Triangle(A.x, A.y, C.x, C.y, O.x, O.y);
                    triangle2 = new Triangle(B.x, B.y, D.x, D.y, O.x, O.y);
                    return Math.Abs(triangle1.Area() - triangle2.Area());
                    break;
                case 4:
                    triangle1 = new Triangle(A.x, A.y, B.x, B.y, O.x, O.y);
                    triangle2 = new Triangle(C.x, C.y, D.x, D.y, O.x, O.y);
                    return Math.Abs(triangle1.Area() - triangle2.Area());
                    break;
                case 5:
                    triangle1 = new Triangle(A.x, A.y, C.x, C.y, O.x, O.y);
                    triangle2 = new Triangle(B.x, B.y, D.x, D.y, O.x, O.y);
                    double test1 = triangle1.Area();
                    double test2 = triangle2.Area();
                    return Math.Abs(triangle1.Area() - triangle2.Area());
                    break;
                case 6:
                    triangle1 = new Triangle(B.x, B.y, C.x, C.y, O.x, O.y);
                    triangle2 = new Triangle(A.x, A.y, D.x, D.y, O.x, O.y);
                    return Math.Abs(triangle1.Area() - triangle2.Area());
                    break;
                default:
                    return 0;
                    break;
            }
        }

        public override bool Equals(object figure)
        {
            if (figure is Trapeze)
            {
                Figure.point O = this.CrossPoint();
                Figure.point O1 = (figure as Trapeze).CrossPoint();
                Figure triangle01;
                Figure triangle02;
                Figure triangle11;
                Figure triangle12;
                Figure.point A1 = (figure as Trapeze).GetA();
                Figure.point B1 = (figure as Trapeze).GetB();
                Figure.point C1 = (figure as Trapeze).GetC();
                Figure.point D1 = (figure as Trapeze).GetD();
                switch (this.TypeOFTrapeze())
                {
                    case 1:
                        triangle01 = new Triangle(A.x, A.y, B.x, B.y, O.x, O.y);
                        triangle02 = new Triangle(C.x, C.y, D.x, D.y, O.x, O.y);
                        break;
                    case 2:
                        triangle01 = new Triangle(B.x, B.y, C.x, C.y, O.x, O.y);
                        triangle02 = new Triangle(A.x, A.y, D.x, D.y, O.x, O.y);
                        break;
                    case 3:
                        triangle01 = new Triangle(A.x, A.y, C.x, C.y, O.x, O.y);
                        triangle02 = new Triangle(B.x, B.y, D.x, D.y, O.x, O.y);
                        break;
                    case 4:
                        triangle01 = new Triangle(A.x, A.y, B.x, B.y, O.x, O.y);
                        triangle02 = new Triangle(C.x, C.y, D.x, D.y, O.x, O.y);
                        break;
                    case 5:
                        triangle01 = new Triangle(A.x, A.y, C.x, C.y, O.x, O.y);
                        triangle02 = new Triangle(B.x, B.y, D.x, D.y, O.x, O.y);
                        break;
                    case 6:
                        triangle01 = new Triangle(B.x, B.y, C.x, C.y, O.x, O.y);
                        triangle02 = new Triangle(A.x, A.y, D.x, D.y, O.x, O.y);
                        break;
                    default:
                        triangle01 = new Triangle(0, 0, 0, 0, 0, 0);
                        triangle02 = new Triangle(0, 0, 0, 0, 0, 0);
                        break;
                }

                switch ((figure as Trapeze).TypeOFTrapeze())
                {
                    case 1:
                        triangle11 = new Triangle(A1.x, A1.y, B1.x, B1.y, O1.x, O1.y);
                        triangle12 = new Triangle(C1.x, C1.y, D1.x, D1.y, O1.x, O1.y);
                        break;
                    case 2:
                        triangle11 = new Triangle(B1.x, B1.y, C1.x, C1.y, O1.x, O1.y);
                        triangle12 = new Triangle(A1.x, A1.y, D1.x, D1.y, O1.x, O1.y);
                        break;
                    case 3:
                        triangle11 = new Triangle(A1.x, A1.y, C1.x, C1.y, O1.x, O1.y);
                        triangle12 = new Triangle(B1.x, B1.y, D1.x, D1.y, O1.x, O1.y);
                        break;
                    case 4:
                        triangle11 = new Triangle(A1.x, A1.y, B1.x, B1.y, O1.x, O1.y);
                        triangle12 = new Triangle(C1.x, C1.y, D1.x, D1.y, O1.x, O1.y);
                        break;
                    case 5:
                        triangle11 = new Triangle(A1.x, A1.y, C1.x, C1.y, O1.x, O1.y);
                        triangle12 = new Triangle(B1.x, B1.y, D1.x, D1.y, O1.x, O1.y);
                        break;
                    case 6:
                        triangle11 = new Triangle(B1.x, B1.y, C1.x, C1.y, O1.x, O1.y);
                        triangle12 = new Triangle(A1.x, A1.y, D1.x, D1.y, O1.x, O1.y);
                        break;
                    default:
                        triangle11 = new Triangle(0, 0, 0, 0, 0, 0);
                        triangle12 = new Triangle(0, 0, 0, 0, 0, 0);
                        break;
                }
                return ((triangle01.Equals(triangle11) && triangle02.Equals(triangle12)) ||
                    (triangle01.Equals(triangle12) && triangle02.Equals(triangle11)));

            }
            else return false;
        }

        public override string ToString()
        {
            return $"трапеция с вершинами в точках A({A.x}; {A.y}) B({B.x}; {B.y}) C({C.x}; {C.y}) D({D.x}; {D.y})";
        }

        public Trapeze(double xa, double ya, double xb, double yb, double xc, double yc, double xd, double yd)
        {
            Figure.point AB;
            Figure.point AC;
            Figure.point AD;
            Figure.point BC;
            Figure.point BD;
            Figure.point CD;
            AB.x = xb - xa;
            AB.y = yb - ya;
            AC.x = xc - xa;
            AC.y = yc - ya;
            AD.x = xd - xa;
            AD.y = yd - ya;
            BC.x = xc - xb;
            BC.y = yc - yb;
            BD.x = xd - xb;
            BD.y = yd - yb;
            CD.x = xd - xc;
            CD.y = yd - yc;
            double ab = Math.Sqrt(Math.Pow(xa - xb, 2) + Math.Pow(ya - yb, 2));
            double ac = Math.Sqrt(Math.Pow(xa - xc, 2) + Math.Pow(ya - yc, 2));
            double ad = Math.Sqrt(Math.Pow(xa - xd, 2) + Math.Pow(ya - yd, 2));
            double bc = Math.Sqrt(Math.Pow(xc - xb, 2) + Math.Pow(yc - yb, 2));
            double bd = Math.Sqrt(Math.Pow(xd - xb, 2) + Math.Pow(yd - yb, 2));
            double cd = Math.Sqrt(Math.Pow(xc - xd, 2) + Math.Pow(yc - yd, 2));

            if ((ab != 0 && ac != 0 && ad != 0 && bc != 0 && bd != 0 && cd != 0) &&
                // если ни один из отрезков не равен 0
                ((AB.x * CD.y == AB.y * CD.x && BC.x * AD.y != BC.y * AD.x) || (AB.x * CD.y != AB.y * CD.x && BC.x * AD.y == BC.y * AD.x) ||
                (AC.x * BD.y == AC.y * BD.x && AB.x * CD.y != AB.y * CD.x) || (AC.x * BD.y != AC.y * BD.x && AB.x * CD.y == AB.y * CD.x) ||
                (AC.x * BD.y == AC.y * BD.x && BC.x * AD.y != BC.y * AD.x) || (AC.x * BD.y != AC.y * BD.x && BC.x * AD.y == BC.y * AD.x))) 
                // и 2 стороны праллельны а другие 2 нет
            {
                A.x = xa;
                A.y = ya;
                B.x = xb;
                B.y = yb;
                C.x = xc;
                C.y = yc;
                D.x = xd;
                D.y = yd;
            }
            else throw new Exception("Такой трапеции не существует");
        }
    }
    public partial class MainWindow : Window
    {
        List<Figure> Figures = new List<Figure>
        {
            new Circle(0, 0, 5),
            new Circle(4.1, -5.14, 40.2),
            new Circle(-11.421, -54.2, 17.2),
            new Rectangle(5, 0, -5, -5, -5, 0, 5, -5),
            new Rectangle(6, -3, 0, 9, 0, -6, -6, 6),
            new Rectangle(0, 0, 1, 0, 1, 5, 0, 5),
            new Trapeze(0, 0, 1, 1, 2, 1, 5, 0),
            new Trapeze(0,-5, 2, -4, 0, 7, 2, 10),
            new Trapeze(10, 0, -10, 0, 0, 5, 10, -10),
        };
        void UpdateFigureList()
        {
            lbFigures.Items.Clear();
            foreach (var figure in Figures) lbFigures.Items.Add(figure);
            lbFigures1.Items.Clear();
            foreach (var figure in Figures) lbFigures1.Items.Add(figure);
        }
        public MainWindow()
        {
            InitializeComponent();
            UpdateFigureList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Figure figure;
                switch (cbFigure.SelectedIndex)
                {
                    case 0:
                        double x = double.Parse(tbCenterX.Text.Replace('.', ','));
                        double y = double.Parse(tbCenterY.Text.Replace('.', ','));
                        double r = double.Parse(tbRadius.Text.Replace('.', ','));
                        figure = new Circle(x, y, r);
                        Figures.Add(figure);
                        UpdateFigureList();
                        break;
                    case 1:
                        double ax = double.Parse(tbAX.Text.Replace('.', ','));
                        double ay = double.Parse(tbAY.Text.Replace('.', ','));
                        double bx = double.Parse(tbBX.Text.Replace('.', ','));
                        double by = double.Parse(tbBY.Text.Replace('.', ','));
                        double cx = double.Parse(tbCX.Text.Replace('.', ','));
                        double cy = double.Parse(tbCY.Text.Replace('.', ','));
                        double dx = double.Parse(tbDX.Text.Replace('.', ','));
                        double dy = double.Parse(tbDY.Text.Replace('.', ','));
                        figure = new Rectangle(ax, ay, bx, by, cx, cy, dx, dy);
                        Figures.Add(figure);
                        UpdateFigureList();
                        break;
                    case 2:
                        ax = double.Parse(tbAX.Text.Replace('.', ','));
                        ay = double.Parse(tbAY.Text.Replace('.', ','));
                        bx = double.Parse(tbBX.Text.Replace('.', ','));
                        by = double.Parse(tbBY.Text.Replace('.', ','));
                        cx = double.Parse(tbCX.Text.Replace('.', ','));
                        cy = double.Parse(tbCY.Text.Replace('.', ','));
                        dx = double.Parse(tbDX.Text.Replace('.', ','));
                        dy = double.Parse(tbDY.Text.Replace('.', ','));
                        figure = new Trapeze(ax, ay, bx, by, cx, cy, dx, dy);
                        Figures.Add(figure);
                        UpdateFigureList();
                        break;
                    default:
                        MessageBox.Show("Выберите фигуру");
                        break;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CbFigure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFigure.SelectedIndex == 0)
            {
                tbCenterX.IsEnabled = true;
                tbCenterY.IsEnabled = true;
                tbRadius.IsEnabled = true;
                tbAX.IsEnabled = false;
                tbAY.IsEnabled = false;
                tbBX.IsEnabled = false;
                tbBY.IsEnabled = false;
                tbCX.IsEnabled = false;
                tbCY.IsEnabled = false;
                tbDX.IsEnabled = false;
                tbDY.IsEnabled = false;
            }
            else
            {
                tbCenterX.IsEnabled = false;
                tbCenterY.IsEnabled = false;
                tbRadius.IsEnabled = false;
                tbAX.IsEnabled = true;
                tbAY.IsEnabled = true;
                tbBX.IsEnabled = true;
                tbBY.IsEnabled = true;
                tbCX.IsEnabled = true;
                tbCY.IsEnabled = true;
                tbDX.IsEnabled = true;
                tbDY.IsEnabled = true;
            }
        }

        private void LbFigures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFigures.SelectedIndex != -1)
            {
                btnRemove.IsEnabled = true;
                btnPerimeter.IsEnabled = true;
                btnArea.IsEnabled = true;
                if (lbFigures1.SelectedIndex != -1) btnEquals.IsEnabled = true;
                else btnEquals.IsEnabled = false;
            }
            else
            {
                btnRemove.IsEnabled = false;
                btnPerimeter.IsEnabled = false;
                btnArea.IsEnabled = false;
                btnEquals.IsEnabled = false;
            }
        }
        private void LbFigures1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFigures1.SelectedIndex != -1 && lbFigures.SelectedIndex != -1) btnEquals.IsEnabled = true;
            else btnEquals.IsEnabled = false;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            Figures.Remove(Figures[lbFigures.SelectedIndex]);
            UpdateFigureList();
        }

        private void BtnPerimeter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Периметр фигуры равен {Figures[lbFigures.SelectedIndex].Perimeter()}");
        }

        private void BtnArea_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Площадь фигуры равна {Figures[lbFigures.SelectedIndex].Area()}");
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (Figures[lbFigures.SelectedIndex].Equals(Figures[lbFigures1.SelectedIndex])) MessageBox.Show("Фигуры равны");
            else MessageBox.Show("Фигуры не равны");
        }

    }
}
