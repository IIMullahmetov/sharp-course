using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;


namespace App
{
    public partial class Form1 : Form
    {
        private Excel.Application excelApp = null;
        private Excel.Worksheet currentSheet = null;

        private string filePath = null; //путь к файлу
        private char startRow = 'E'; //столбец откуда начинаются данные
        private int startIndex = 5; //строка откуда начинаются данные
        private char endRow = 'O'; //столбец где кончаются данные  
        private const int PARAM_COUNT = 9; //количество параметров у одного кадра (индекс не считаем) (+ подозрения на повтор 11 поля)

        private double[] param = new double[PARAM_COUNT];
        private double[,] paramPlus = new double[5, PARAM_COUNT];

        const int Gt = 0;
        const int t6 = 1;
        const int aPT = 2;
        const int nBD = 3;
        const int iPT = 4;
        const int iSTKR = 5;

        //дальше неясно
        const int mode = 6; /// ?????
        const int iOST = 7; /// ?????
        const int iESUD = 8; /// ?????
        const int iPHA = 9; /// ?????

        //переменные
        private int t = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //константы 
            //порядковый номер параметра в файле контроля
            /*
            const int Gt = 0;
            const int mode = 0;
            const int nBD = 0;
            const int t6 = 0;
            const int aPT = 0;
            const int iSTKR = 0;
            const int iOST = 0;
            const int iPT = 0;
            const int iESUD = 0;
            const int iPHA = 0;
            */

            // диагностические сообщения
            string[] DS = new string[31]
            {
                "",//0
                "",//1
                "Топливо при запуске подано по времени при n_вд<1330 об/мин",//2
                "Нет восламенения топлива",//3
                "Неисправность ЭСУД. Сигнал \"останов по перегреву\"",//4
                "Сигнал минимальное давление топлива",//5
                "Перегрев",//6
                "Превышена настройка РСТ",//7
                "",//8
                "Преждевременная подача топлива при запуске при n_вд<1330 об/мин",//9
                "",//10
                "",//11
                "Двигатель выключен при холодном зависании",//12
                "Сигнал ЭСУД не работает",//13
                "",//14
                "П_(вд мг)  меньше нормы. Повернуть винт А1 АДТ на К щелчков по часовой стрелке",//15
                "Время запуска больше нормы",//16
                "Ложный останов двигателя ЭСУД. Проверить настройку РСТ",//17
                "Двигатель не включен",//18
                "Перемещение РУД при запуске",//19
                "Запуск не выполнен. Нет подачи топлива",//20
                "При запуске прекращена подача топлива",//21
                "Запуск прекращен стоп-краном",//22
                "Останов по перегреву",//23
                "",//24
                "",//25
                "Контроль САР на режиме малого газа не проводился. РУД вне площадки малого газа",//26
                "Контроль САР на режиме малого газа не проводился. Некачественная информация",//27
                "",//28
                "",//29
                "Запуск не контролировался",//30
            };

            List<string> diagnostics = new List<string>();

            //const int t6_start = 0; //определить
            const double t6_start = 0.0; //определить
            //const int t6_max = 530;
            const double t6_max = 530.0;
            //int aPT_bas = 0;
            double aPT_bas = 0.0;
            /*
            //переменные
            int t = 0;
            */
            //int[] param = new int[50];
            //int[,] paramPlus = new int[50, 5];

            int P1 = 0;
            int P2 = 0;
            int D21 = 0;
            int D31 = 0;

            bool FuelInj = false;
            bool FuelBurnTest = false;




            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath))
            {
                try
                {
                    excelApp = new Excel.Application() { Visible = false };
                    excelApp.Workbooks.Open(filePath);
                    currentSheet = (Excel.Worksheet)excelApp.Workbooks[1].Worksheets[1];
                }
                catch
                {
                    excelApp.Quit();
                }
            }

            // считываем из текстового файла 1 кадр в массив param
            SetOneFrame(t);
            // считываем из текстового файла еще 5 кадров в массив paramPlus 
            SetFiveFrame(t);

            //1
            //if ((param[Gt] >= 600) & (paramPlus[Gt, 0] >= 600) & (paramPlus[Gt, 1] >= 600) & (paramPlus[Gt, 2] >= 600))
            if ((param[Gt] >= 600.0) & (paramPlus[Gt, 0] >= 600.0) & (paramPlus[Gt, 1] >= 600.0) & (paramPlus[Gt, 2] >= 600.0))

            {
                //textBox2.Text = textBox2.Text + '\n' + DS[30];
                diagnostics.Add(DS[30]);
                textBox2.Lines = diagnostics.ToArray();
                //while (param[mode] != 1)
                while (param[mode] != 1.0)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    SetOneFrame(t);
                    // считываем из текстового файла еще 5 кадров в массив paramPlus 
                    SetFiveFrame(t);
                }
            }

            // РЕЖИМ "СТАРТ"

            //while (param[mode] != 1)
            while (param[mode] != 1.0)
            {
                //2
                //if ((param[nBD] < 0) | (param[nBD] > 5000) | (param[t6] < -60) | (param[t6] > 600) | (param[Gt] < 0) | (param[Gt] > 600))
                if ((param[nBD] < 0.0) | (param[nBD] > 5000.0) | (param[t6] < -60.0) | (param[t6] > 600.0) | (param[Gt] < 0.0) | (param[Gt] > 600.0))
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    SetOneFrame(t);
                    // считываем из текстового файла еще 5 кадров в массив paramPlus 
                    SetFiveFrame(t);
                }

                //3
                /*
                if (((paramPlus[aPT, 0] < 25) | (paramPlus[aPT, 0] > 40)) & ((paramPlus[aPT, 1] < 25) | (paramPlus[aPT, 1] > 40)) & ((paramPlus[aPT, 2] < 25) | (paramPlus[aPT, 2] > 40))
                            & ((paramPlus[aPT, 3] < 25) | (paramPlus[aPT, 3] > 40)) & ((paramPlus[aPT, 4] < 25) | (paramPlus[aPT, 4] > 40)) & ((paramPlus[aPT, 5] < 25) | (paramPlus[aPT, 5] > 40)))
                            */
                if (((paramPlus[aPT, 0] < 25.0) | (paramPlus[aPT, 0] > 40.0)) & ((paramPlus[aPT, 1] < 25.0) | (paramPlus[aPT, 1] > 40.0)) & ((paramPlus[aPT, 2] < 25.0) | (paramPlus[aPT, 2] > 40.0))
                    & ((paramPlus[aPT, 3] < 25.0) | (paramPlus[aPT, 3] > 40.0)) & ((paramPlus[aPT, 4] < 25.0) | (paramPlus[aPT, 4] > 40.0)) & ((paramPlus[aPT, 5] < 25.0) | (paramPlus[aPT, 5] > 40.0)))
                {
                    //int aPT_mean = (param[aPT] + paramPlus[aPT, 0] + paramPlus[aPT, 1] + paramPlus[aPT, 2] + paramPlus[aPT, 3] + paramPlus[aPT, 4] + paramPlus[aPT, 5]) / 6;
                    double aPT_mean = (param[aPT] + paramPlus[aPT, 0] + paramPlus[aPT, 1] + paramPlus[aPT, 2] + paramPlus[aPT, 3] + paramPlus[aPT, 4] + paramPlus[aPT, 5]) / 6.0;
                    //textBox2.Text = textBox2.Text + '\n' + DS[8] + aPT_mean;
                    diagnostics.Add(DS[8]);
                    textBox2.Lines = diagnostics.ToArray();
                }

                //4
                //if ((param[Gt] < 260) & (paramPlus[Gt, 0] < 260) & (paramPlus[Gt, 1] < 260) & (paramPlus[Gt, 2] < 260) & (paramPlus[Gt, 3] < 260))
                if ((param[Gt] < 260.0) & (paramPlus[Gt, 0] < 260.0) & (paramPlus[Gt, 1] < 260.0) & (paramPlus[Gt, 2] < 260.0) & (paramPlus[Gt, 3] < 260.0))
                    //проверить была ли подача топлива (непонятно как?)
                    if (FuelInj)
                    {
                        diagnostics.Add(DS[30]);
                        textBox2.Lines = diagnostics.ToArray();
                        //textBox2.Text = textBox2.Text + '\n' + DS[30];
                    }
                
                    else if (t < 36)
                    {
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        SetOneFrame(t);
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        SetFiveFrame(t);
                        //if (param[iSTKR] == 1) return; else continue;
                        if (param[iSTKR] == 1.0) return;
                        else continue;
                    }
                    else
                    {
                        diagnostics.Add(DS[20]);
                        textBox2.Lines = diagnostics.ToArray();
                        //textBox2.Text = textBox2.Text + '\n' + DS[20];
                    }
                else goto L8;

                //5
                //if (param[iSTKR] == 1)
                if (param[iSTKR] == 1.0)
                {
                    diagnostics.Add(DS[22]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[22];
                    return;
                }

                //6
                //if (param[iOST] == 1)
                if (param[iOST] == 1.0)
                {
                    diagnostics.Add(DS[4]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[4];
                    return;
                }

                //7
                //if (param[iPT] == 1)
                if (param[iPT] == 1.0)
                {
                    diagnostics.Add(DS[5]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[5];
                    return;
                }

                //8 
                L8:
                //if ((param[Gt] >= 260) & (paramPlus[Gt, 0] >= 260) & (paramPlus[Gt, 1] >= 260) & (paramPlus[Gt, 2] >= 260) & (paramPlus[Gt, 3] >= 260))
                if ((param[Gt] >= 260.0) & (paramPlus[Gt, 0] >= 260.0) & (paramPlus[Gt, 1] >= 260.0) & (paramPlus[Gt, 2] >= 260.0) & (paramPlus[Gt, 3] >= 260.0))
                {
                    label2.Text = label2.Text + '\n' + t + "с - Топливо подано";
                    //считать файл от первого кадра до t, определить aPT_mean (среднее)
                    double aPT_mean = GetAverageApt();
                    //int Gt_mean = (param[Gt] + paramPlus[Gt, 0] + paramPlus[Gt, 1] + paramPlus[Gt, 2] + paramPlus[Gt, 3]) / 4;
                    double Gt_mean = (param[Gt] + paramPlus[Gt, 0] + paramPlus[Gt, 1] + paramPlus[Gt, 2] + paramPlus[Gt, 3]) / 4;
                    label2.Text = label2.Text + '\n' + "Средняя величина расхода на площадке розжига " + Gt_mean;
                }

                //9
                if (!FuelBurnTest)

                    //10
                    //if (param[nBD] < 1330)
                    if (param[nBD] < 1330.0)
                        if (t < 26)
                        {
                            diagnostics.Add(DS[9]);
                            textBox2.Lines = diagnostics.ToArray();
                            //textBox2.Text = textBox2.Text + '\n' + DS[9];
                        }
                        else
                        {
                            diagnostics.Add(DS[2]);
                            textBox2.Lines = diagnostics.ToArray();
                            //textBox2.Text = textBox2.Text + '\n' + DS[2];
                        }

                //11
                //if (param[t6] < 110)
                if (param[t6] < 110.0)
                    if (t > 36)
                    {
                        diagnostics.Add(DS[3]);
                        textBox2.Lines = diagnostics.ToArray();
                        //textBox2.Text = textBox2.Text + '\n' + DS[3];
                        return;
                    }
                    else
                    {
                        //??
                        t++;
                        continue;
                    }

                //12
                //int dt6PCT;
                double dt6PCT;
                L12:
                //if (param[iOST] == 1)
                if (param[iOST] == 1.0)
                {
                    diagnostics.Add(DS[23]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[23];

                    //13
                    dt6PCT = param[t6] - t6_start;
                    //if (dt6PCT < 10)
                    if (dt6PCT < 10.0)
                    {
                        diagnostics.Add(DS[17]);
                        textBox2.Lines = diagnostics.ToArray();
                        //textBox2.Text = textBox2.Text + '\n' + DS[17];
                    }
                    //if (dt6PCT >= 30)
                    if (dt6PCT >= 30.0)
                    {
                        diagnostics.Add(DS[7]);
                        textBox2.Lines = diagnostics.ToArray();
                        //textBox2.Text = textBox2.Text + '\n' + DS[7];
                    }
                    return;
                }

                //14
                //if (param[iESUD] == 1)
                if (param[iESUD] == 1.0)
                {
                    diagnostics.Add(DS[13]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[13];
                }

                //15
                L18:
                dt6PCT = param[t6] - t6_start;
                //if (dt6PCT >= 30)
                if (dt6PCT >= 30.0)
                {

                    //16
                    //if (((param[t6] - t6_start) >= 30) & ((paramPlus[t6, 0] - t6_start) >= 30) & ((paramPlus[t6, 1] - t6_start) >= 30))
                    if (((param[t6] - t6_start) >= 30.0) & ((paramPlus[t6, 0] - t6_start) >= 30.0) & ((paramPlus[t6, 1] - t6_start) >= 30.0))

                        //17
                        //if ((param[Gt] >= 260) & (paramPlus[Gt, 0] >= 260) & (paramPlus[Gt, 1] >= 260) & (paramPlus[Gt, 2] >= 260) & (paramPlus[Gt, 3] >= 260))
                        if ((param[Gt] >= 260.0) & (paramPlus[Gt, 0] >= 260.0) & (paramPlus[Gt, 1] >= 260.0) & (paramPlus[Gt, 2] >= 260.0) & (paramPlus[Gt, 3] >= 260.0))
                        {
                            if (param[iESUD] != 1)
                            {
                                diagnostics.Add(DS[18]);
                                textBox2.Lines = diagnostics.ToArray();
                                //textBox2.Text = textBox2.Text + '\n' + DS[18];
                            }

                        }
                        else return;

                    //18
                    if (param[t6] > t6_max)
                    { /// ?????
                        //определить продолжительность заброса t_exc и максимальное значение температуры t6_exc
                        int t6_exc = 0;
                        int t_exc = 0;
                        diagnostics.Add("Максимальное значение температуры t6max, град " + t6_exc + " и суммарное время заброса (сек) " + t_exc);
                        textBox2.Lines = diagnostics.ToArray();
                        //label2.Text = label2.Text + '\n' + "Максимальное значение температуры t6max, град " + t6_exc + " и суммарное время заброса (сек) " + t_exc;
                    }
                }

                //19
                //if (param[nBD] >= 4000) goto L29;
                if (param[nBD] >= 4000.0) goto L29;

                //20
                // Определить P1 - количество кадров , в которых nBD<4000 
                L20:
                P1 = GetNbdCount(4000.0, false);

                L21:
                //int dnBD = 0, dt6 = 0;
                double dnBD = 0.0, dt6 = 0.0;

                if (P1 == 1)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    SetOneFrame(t);
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    SetFiveFrame(t);

                    //21
                    //if (param[iSTKR] == 1) goto L12; else return;
                    if (param[iSTKR] == 1.0) goto L12; else return;
                }
                else
                {
                    dnBD = param[nBD] - paramPlus[nBD, 0];
                    dt6 = param[t6] - paramPlus[t6, 0];
                }

                //22
                //if (dnBD >= 50)
                if (dnBD >= 50.0)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    SetOneFrame(t);
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    SetFiveFrame(t);
                    goto L12;
                }
                else
                {
                    //посчитить количество кадров P2, в которых dnBD >= 50
                    P2 = GetNbdCount(50, true);
                }

                //23
                //if (dt6 < 30) { }
                if (dt6 < 30.0) { }
                //запоминть Д21  /// ?????
                else
                    //запомнить Д31  /// ?????

                    //24
                if (P2 < 7)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    SetOneFrame(t);
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    SetFiveFrame(t);
                    goto L21;
                }

                //25
                /*
                if (((param[t6] - paramPlus[t6, 0]) >= 5) & ((paramPlus[t6, 0] - paramPlus[t6, 1]) >= 5) & ((paramPlus[t6, 1] - paramPlus[t6, 2]) >= 5)
                    & ((paramPlus[t6, 2] - paramPlus[t6, 3]) >= 5) & ((paramPlus[t6, 3] - paramPlus[t6, 4]) >= 5) & ((paramPlus[t6, 4] - paramPlus[t6, 5]) >= 5))
                    */
                if (((param[t6] - paramPlus[t6, 0]) >= 5.0) & ((paramPlus[t6, 0] - paramPlus[t6, 1]) >= 5.0) & ((paramPlus[t6, 1] - paramPlus[t6, 2]) >= 5.0)
                    & ((paramPlus[t6, 2] - paramPlus[t6, 3]) >= 5.0) & ((paramPlus[t6, 3] - paramPlus[t6, 4]) >= 5.0) & ((paramPlus[t6, 4] - paramPlus[t6, 5]) >= 5.0))
                {
                    //26
                    t = t + 5;
                    // считываем из текстового файла 1 кадр в массив param
                    SetOneFrame(t);
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    SetFiveFrame(t);
                    //if (param[iSTKR] == 1)
                    if (param[iSTKR] == 1.0)
                    {
                        if (t > 76)
                        {
                            diagnostics.Add(DS[12]);
                            textBox2.Lines = diagnostics.ToArray();
                            //textBox2.Text = textBox2.Text + '\n' + DS[12];
                        }
                    }
                    else
                    {
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        SetOneFrame(t);
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        SetFiveFrame(t);
                        goto L12;
                    }
                }
                else
                { /// ?????
                    //проверить по этим же кадрам значений dnBD>=50 и dt6>=30 (D31)
                    if (D31 < 5)
                    {
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        SetOneFrame(t);
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        SetFiveFrame(t);
                        goto L12;
                    }
                }


                //27
                L29:
                //int daPT = (param[aPT] + paramPlus[aPT, 0] + paramPlus[aPT, 1] + paramPlus[aPT, 2]) / 4 - aPT_bas;
                double daPT = (param[aPT] + paramPlus[aPT, 0] + paramPlus[aPT, 1] + paramPlus[aPT, 2]) / 4 - aPT_bas;

                //if (daPT >= 2)
                if (daPT >= 2.0)
                {
                    diagnostics.Add(DS[19]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[19];

                    //28
                    //if (param[iPHA] == 0)
                    if (param[iPHA] == 0.0)
                    {
                        diagnostics.Add(DS[10]);
                        textBox2.Lines = diagnostics.ToArray();
                        //textBox2.Text = textBox2.Text + '\n' + DS[10];
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        SetOneFrame(t);
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        SetFiveFrame(t);
                        goto L21;
                    }
                    else goto L31;
                }
                else
                {
                    //29
                    //if (param[nBD] >= 4000)
                    if (param[nBD] >= 4000.0)
                        //if (param[mode] == 0)
                        if (param[mode] == 0.0)
                        {
                            //30
                            /*
                            if (((param[nBD] - paramPlus[nBD, 0]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) &
                                ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10))
                                */
                            if (((param[nBD] - paramPlus[nBD, 0]) < 10.0) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10.0) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10.0)
                                & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10.0) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10.0) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10.0))
                            {
                                label2.Text = label2.Text + '\n' + "Время запуска двигателя, с " + t;
                                if (t <= 76)
                                {
                                    t++;
                                    // считываем из текстового файла 1 кадр в массив param
                                    SetOneFrame(t);
                                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                                    SetFiveFrame(t);
                                    goto L18;
                                }
                                else
                                {
                                    diagnostics.Add(DS[16]);
                                    textBox2.Lines = diagnostics.ToArray();
                                    //textBox2.Text = textBox2.Text + '\n' + DS[16];
                                }
                            }
                        }
                        else goto L32; //переход к режиму МГ 
                }

                //31
                L32:
                L31:
                //if (param[iSTKR] == 1)
                if (param[iSTKR] == 1.0)
                {
                    diagnostics.Add(DS[22]);
                    textBox2.Lines = diagnostics.ToArray();
                    //textBox2.Text = textBox2.Text + '\n' + DS[22];
                    return;
                }
                else goto L18;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel files(*.xls, *.xlsx)|*.xls*|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = openFileDialog1.FileName;
            filePath = filename;
            textBox1.Text = filename;
        }



        private void SetOneFrame(int row)
        {
            int rowCell = startIndex + row; //строчка в таблице
            if (currentSheet.get_Range(startRow + rowCell.ToString()).Value2 != null)
            {
                ReadFrame(rowCell);
            }
        }

        private void SetFiveFrame(int row)
        {
            int rowPlus = row + 1;
            int rowCell = startIndex + rowPlus; //строчка в таблице
            if (currentSheet.get_Range(startRow + rowCell.ToString()).Value2 != null)
            {
                Console.WriteLine("PARAMPLUS");
                for (int index = 0; index < 5; index++)
                {
                    int curRowCell = index + rowPlus + startIndex; //строчка в таблице
                    ReadFrame(curRowCell, index);
                }
            }
        }

        private void ReadFrame(int rowCell)
        {
            int i = 0;
            try
            {
                Console.WriteLine("PARAM");
                for (var column = startRow; column < endRow && i < PARAM_COUNT; column++)
                {
                    param[i] = Convert.ToDouble(currentSheet.get_Range(column.ToString() + rowCell).Text);
                    Console.Write(param[i] + " ");
                    i++;
                }
                Console.WriteLine();
            }
            catch
            {
                excelApp.Quit();
            }
        }

        private void ReadFrame(int rowCell, int index)
        {
            int i = 0;
            try
            {
                for (var column = startRow; column < endRow && i < PARAM_COUNT; column++)
                {
                    paramPlus[index, i] = Convert.ToDouble(currentSheet.get_Range(column.ToString() + rowCell).Text);
                    Console.Write(paramPlus[index, i] + " ");
                    i++;
                }
                Console.WriteLine();
            }
            catch
            {
                excelApp.Quit();
            }
        }

        private double GetAverageApt()
        {
            double[] temp = new double[t];
            try
            {
                for (int index = 0; index < t; index++)
                {
                    string cell = string.Concat(((char)(startRow + aPT)).ToString(), (startIndex + index).ToString());
                    if (currentSheet.get_Range(cell).Value2 != null)
                    {
                        temp[index] = ReadApt(cell);
                    }
                }
                return temp.Average();
            }
            catch
            {
                excelApp.Quit();
            }
            return 0.0;
        }

        private double ReadApt(string cell)
        {
            try
            {
                return Convert.ToDouble(currentSheet.get_Range(cell).Text);
            }
            catch
            {
                excelApp.Quit();
            }
            return 0.0;
        }

        private int GetNbdCount(double value, bool moreOrEqual)
        {
            int count = 0;
            string cell = null;
            try
            {
                for (int index = 0; true; index++)
                {
                    cell = string.Concat(((char)(startRow + nBD)).ToString(), (startIndex + index).ToString());
                    if (currentSheet.get_Range(cell).Value2 != null)
                    {
                        if (!moreOrEqual && ReadNbd(cell) < value)
                            count++;
                        else if (moreOrEqual && ReadNbd(cell) >= value)
                            count++;
                    }
                    else
                        break;
                }
                return count;
            }
            catch
            {
                excelApp.Quit();
            }
            return 0;
        }

        private double ReadNbd(string cell)
        {
            try
            {
                return Convert.ToDouble(currentSheet.get_Range(cell).Text);
            }
            catch
            {
                excelApp.Quit();
            }
            return 0.0;
        }

   
    }
}
