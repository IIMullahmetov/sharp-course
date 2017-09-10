﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //константы 
            //порядковый номер параметра в файле контроля
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


            // диагностические сообщения
            string[] DS = new string[30];

            const int t6_start = 0; //определить
            const int t6_max = 530;
            int aPT_bas = 0;

            //переменные
            int t = 0;
            int[] param = new int[50];
            int[,] paramPlus = new int[50,5];
            int P1 = 0;
            int P2 = 0;
            int D21 = 0;
            int D31 = 0;



            bool FuelInj = false;
            bool FuelBurnTest = false;

            // считываем из текстового файла 1 кадр в массив param
            // считываем из текстового файла еще 5 кадров в массив paramPlus 

            //1
            if ((param[Gt] >= 600) & (paramPlus[Gt, 0] >= 600) & (paramPlus[Gt, 1] >= 600) & (paramPlus[Gt, 2] >= 600))
            {
                label1.Text = label1.Text + '\n' + DS[30];
                while (param[mode] != 1)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    // считываем из текстового файла еще 5 кадров в массив paramPlus 
                }
            }


            // РЕЖИМ "СТАРТ"

            while (param[mode] != 1)
            {
                //2
                if ((param[nBD] < 0) | (param[nBD] > 5000) | (param[t6] < -60) | (param[t6] > 600) | (param[Gt] < 0) | (param[Gt] > 600))
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    // считываем из текстового файла еще 5 кадров в массив paramPlus 
                }

                //3
                if (((paramPlus[aPT, 0] < 25) | (paramPlus[aPT, 0] > 40)) & ((paramPlus[aPT, 1] < 25) | (paramPlus[aPT, 1] > 40)) & ((paramPlus[aPT, 2] < 25) | (paramPlus[aPT, 2] > 40))
                            & ((paramPlus[aPT, 3] < 25) | (paramPlus[aPT, 3] > 40)) & ((paramPlus[aPT, 4] < 25) | (paramPlus[aPT, 4] > 40)) & ((paramPlus[aPT, 5] < 25) | (paramPlus[aPT, 5] > 40)))
                {
                    int aPT_mean = (param[aPT] + paramPlus[aPT, 0] + paramPlus[aPT, 1] + paramPlus[aPT, 2] + paramPlus[aPT, 3] + paramPlus[aPT, 4] + paramPlus[aPT, 5]) / 6;
                    label1.Text = label1.Text + '\n' + DS[8] + aPT_mean;
                }

                //4
                if ((param[Gt] < 260) & (paramPlus[Gt, 0] < 260) & (paramPlus[Gt, 1] < 260) & (paramPlus[Gt, 2] < 260) & (paramPlus[Gt, 3] < 260))
                    //проверить была ли подача топлива (непонятно как?)
                    if (FuelInj) label1.Text = label1.Text + '\n' + DS[30];
                    else if (t < 36)
                    {
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        if (param[iSTKR] == 1) return; else continue;
                    }
                    else label1.Text = label1.Text + '\n' + DS[20];
                else goto L8;

                //5
                if (param[iSTKR] == 1)
                {
                    label1.Text = label1.Text + '\n' + DS[22];
                    return;
                }

                //6
                if (param[iOST] == 1)
                {
                    label1.Text = label1.Text + '\n' + DS[4];
                    return;
                }

                //7
                if (param[iPT] == 1)
                {
                    label1.Text = label1.Text + '\n' + DS[5];
                    return;
                }

                //8 
                L8:
                if ((param[Gt] >= 260) & (paramPlus[Gt, 0] >= 260) & (paramPlus[Gt, 1] >= 260) & (paramPlus[Gt, 2] >= 260) & (paramPlus[Gt, 3] >= 260))
                {
                    label2.Text = label2.Text + '\n' + t + "с - Топливо подано";
                    //считать файл от первого кадра до t, определить aPT_mean (среднее)
                    int Gt_mean = (param[Gt] + paramPlus[Gt, 0] + paramPlus[Gt, 1] + paramPlus[Gt, 2] + paramPlus[Gt, 3]) / 4;
                    label2.Text = label2.Text + '\n' + "Средняя величина расхода на площадке розжига " + Gt_mean;
                }

                //9
                if (!FuelBurnTest) 
                
                //10
                    if (param[nBD] < 1330) 
                        if (t<26) label1.Text = label1.Text + '\n' + DS[9];
                        else label1.Text = label1.Text + '\n' + DS[2];

                //11
                if (param[t6] < 110)
                    if (t > 36)
                    {
                        label1.Text = label1.Text + '\n' + DS[3];
                        return;
                    }
                    else continue;

                //12
                int dt6PCT;
                L12:
                if (param[iOST] == 1)
                {
                    label1.Text = label1.Text + '\n' + DS[23];

                    //13
                    dt6PCT = param[t6] - t6_start;
                    if (dt6PCT < 10)
                        label1.Text = label1.Text + '\n' + DS[17];
                    if (dt6PCT >= 30)
                        label1.Text = label1.Text + '\n' + DS[7];
                    return;
                }

                //14
                if (param[iESUD] == 1)
                    label1.Text = label1.Text + '\n' + DS[13];

                //15
                L18:
                dt6PCT = param[t6] - t6_start;
                if (dt6PCT >= 30)
                {

                    //16
                    if (((param[t6] - t6_start) >= 30) & ((paramPlus[t6, 0] - t6_start) >= 30) & ((paramPlus[t6, 1] - t6_start) >= 30))

                        //17
                        if ((param[Gt] >= 260) & (paramPlus[Gt, 0] >= 260) & (paramPlus[Gt, 1] >= 260) & (paramPlus[Gt, 2] >= 260) & (paramPlus[Gt, 3] >= 260))
                        {
                            if (param[iESUD] != 1)
                                label1.Text = label1.Text + '\n' + DS[18];

                        }
                        else return;
                    
                    //18
                    if (param[t6] > t6_max)
                    {
                        //определить продолжительность заброса t_exc и максимальное значение температуры t6_exc
                        int t6_exc = 0;
                        int t_exc = 0;
                        label2.Text = label2.Text + '\n' + "Максимальное значение температуры t6max, град " + t6_exc + " и суммарное время заброса (сек) " + t_exc;
                    }
                }

                //19
                if (param[nBD] >= 4000) goto L29;

                //20
                // Определить P1 - количество кадров , в которых nBD<4000 
                L21:
                int dnBD = 0, dt6 = 0;

                if (P1 == 1)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    // считываем из текстового файла еще 5 кадров в массив paramPlus

                    //21
                    if (param[iSTKR] == 1) goto L12; else return;
                }
                else
                {
                    dnBD = param[nBD] - paramPlus[nBD, 0];
                    dt6 = param[t6] - paramPlus[t6, 0];
                }

                //22
                if (dnBD >= 50)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    goto L12;
                }
                else
                {
                    //посчитить количество кадров P2, в которых dnBD >= 50
                }

                //23
                if (dt6 < 30) { }
                //запоминть Д21
                else
                //запомнить Д31

                //24
                if (P2<7)
                {
                    t++;
                    // считываем из текстового файла 1 кадр в массив param
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    goto L21;
                }

                //25
                if (((param[t6] - paramPlus[t6, 0]) >= 5) & ((paramPlus[t6, 0] - paramPlus[t6, 1]) >= 5) & ((paramPlus[t6, 1] - paramPlus[t6, 2]) >= 5)
                    & ((paramPlus[t6, 2] - paramPlus[t6, 3]) >= 5) & ((paramPlus[t6, 3] - paramPlus[t6, 4]) >= 5) & ((paramPlus[t6, 4] - paramPlus[t6, 5]) >= 5))
                {
                    //26
                    t = t + 5;
                    // считываем из текстового файла 1 кадр в массив param
                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                    if (param[iSTKR] == 1)
                    {
                        if (t > 76)
                            label1.Text = label1.Text + '\n' + DS[12];
                    }
                    else
                    {
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        goto L12;
                    }
                }
                else
                {
                    //проверить по этим же кадрам значений dnBD>=50 и dt6>=30 (D31)
                    if (D31 < 5)
                    {
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        goto L12;
                    }
                }

      
                //27
                L29:
                int daPT = (param[aPT] + paramPlus[aPT, 0] + paramPlus[aPT, 1] + paramPlus[aPT, 2]) / 4 - aPT_bas;
                if (daPT >= 2)
                {
                    label1.Text = label1.Text + '\n' + DS[19];

                    //28
                    if (param[iPHA] == 0)
                    {
                        label1.Text = label1.Text + '\n' + DS[10];
                        t++;
                        // считываем из текстового файла 1 кадр в массив param
                        // считываем из текстового файла еще 5 кадров в массив paramPlus
                        goto L21;
                    }
                    else goto L31;
                }
                else
                {
                    //29
                    if (param[nBD] >= 4000)
                        if (param[mode] == 0)
                        {
                            //30
                            if (((param[nBD] - paramPlus[nBD, 0]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) &
                                ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10) & ((paramPlus[nBD, 0] - paramPlus[nBD, 1]) < 10))
                            {
                                label2.Text = label2.Text + '\n' + "Время запуска двигателя, с " + t;
                                if (t <= 76)
                                {
                                    t++;
                                    // считываем из текстового файла 1 кадр в массив param
                                    // считываем из текстового файла еще 5 кадров в массив paramPlus
                                    goto L18;
                                }
                                else
                                    label1.Text = label1.Text + '\n' + DS[16];
                            }
                        }
                       else goto L32; //переход к режиму МГ 
                }

            //31
            L32:
            L31:
                if (param[iSTKR] == 1)
                {
                    label1.Text = label1.Text + '\n' + DS[22];
                    return;
                }
                else goto L18;
            }

        }
    }
}
