using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinPacked
{
    public partial class Form1 : Form
    {
        public List<Bin> InitalList = new List<Bin>();
       public  List<Bin> FinallListNormaly = new List<Bin>();
       public int c = 50;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecNormally();

        }

        public void ExecNormally()
        {
           InitalList = new List<Bin>
            {
                new Bin{ ID = 1, BinWeight=30 },
                new Bin{ ID = 2, BinWeight=25},
                new Bin{ ID = 3, BinWeight=10 },
                 new Bin{ ID = 4, BinWeight=20 },
                  new Bin{ ID = 5, BinWeight=15 }
            };

           
            int n = InitalList.Count;

            List<Bin> Final = nextFit(InitalList, n, c);

            FillInitialList(InitalList);
            FillListAfterGeneration(Final);
            FillTotalBins(Final);

        }


        private void FillTotalBins(List<Bin> final)
        {
            var list = final.GroupBy(elem => elem.Actual_Bin).Select(x=>x.First()).ToList();
            label6.Text = list.Count.ToString();
        }

        private void FillListAfterGeneration(List<Bin> final)
        {

            final = final.OrderBy(x => x.ID).ToList();
            FinallListNormaly = final;
            string anser = "";
            foreach (var item in final)
            {
                anser = anser + "      " +item.Actual_Bin;
            }

            label4.Text = anser;
        }

        private void FillInitialList(List<Bin> initalList)
        {
            string answer = "";
            foreach (var item in initalList)
            {
                answer = answer + "    " + item.BinWeight;

            }
            label2.Text = answer;
        }

        List<Bin> nextFit(List<Bin> weightParam, int n, int c)
        {
            List<Bin> LisofBins = new List<Bin>();

            List<Bin> weight = weightParam;
            weight = weight.OrderBy(a => Guid.NewGuid()).ToList();


            int bin_rem = c;
            int actual_bin = 1;


            for (int i = 0; i < n; i++)
            {

                if (weight[i].BinWeight > bin_rem)
                {
                    Bin b = new Bin();


                    actual_bin = actual_bin + 1;

                    b.ID = weight[i].ID;
                    b.BinWeight= weight[i].BinWeight;
                    b.Actual_Bin = actual_bin;
                    LisofBins.Add(b);


                    bin_rem = c - weight[i].BinWeight;
                    
                   
                }
                else
                {
                    bin_rem -= weight[i].BinWeight;

                    Bin b = new Bin();
                    b.ID = weight[i].ID;
                    b.BinWeight = weight[i].BinWeight;
                    b.Actual_Bin = actual_bin;
                    LisofBins.Add(b);

                }

               


            }
            return LisofBins;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FinallListNormaly = FinallListNormaly.OrderBy(x => x.ID).ToList();
            if (FinallListNormaly.Count==0)
            {
                MessageBox.Show("Ju lutem gjeneorni se pari zgjidhjen fillestare");

            }
            else
            {

                List<Bin> actualList = new List<Bin>();
                actualList= FinallListNormaly;
                List<int> valid_bins = new List<int>();

              
                Bin _temp= actualList.OrderBy(a => Guid.NewGuid()).ToList().FirstOrDefault();

                var allbins = actualList.GroupBy(elem => elem.Actual_Bin).Select(x => x.First()).ToList();
                foreach (var item in allbins)
                {
                    var acttual_size = actualList.Where(x => x.Actual_Bin == item.Actual_Bin).Sum(x => x.BinWeight);
                    if((c- acttual_size)>= _temp.BinWeight && item.Actual_Bin != _temp.Actual_Bin)
                    {
                        valid_bins.Add(item.Actual_Bin);
                    }

                }

                // nese nuk ka Vlera tjera
                if(valid_bins.Count>0)
                {
                    int bin_to_add = valid_bins.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    _temp.Actual_Bin = bin_to_add;
                    actualList.RemoveAll(x => x.ID == _temp.ID);
                    actualList.Add(_temp);



                }

                // nese ka mundesi me e vendos dikund tjeter
                else
                {
                    int maxbin = allbins.Max(x => x.Actual_Bin);
                    _temp.Actual_Bin = maxbin+1;
                    actualList.RemoveAll(x => x.ID == _temp.ID);
                    actualList.Add(_temp);


                }

                FillInitialListWithMutation(FinallListNormaly);
                FillListAfterGenerationWithMutation(actualList);
                FillTotalBinsAfterMutation(actualList);
               FinallListNormaly = new List<Bin>();

            }
        }

        private void FillInitialListWithMutation(List<Bin> initalList)
        {
            initalList = initalList.OrderBy(x => x.ID).ToList();
            string answer = "";
            foreach (var item in initalList)
            {
                answer = answer + "    " + item.BinWeight;

            }
            label11.Text = answer;
        }

        private void FillListAfterGenerationWithMutation(List<Bin> final)
        {

            final = final.OrderBy(x => x.ID).ToList();
            FinallListNormaly = final;
            string anser = "";
            foreach (var item in final)
            {
                anser = anser + "      " + item.Actual_Bin;
            }

            label9.Text = anser;
        }

        private void FillTotalBinsAfterMutation(List<Bin> final)
        {
            var list = final.GroupBy(elem => elem.Actual_Bin).Select(x => x.First()).ToList();
            label7.Text = list.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Bin> Crossoverlist = new List<Bin>();

            InitalList = new List<Bin>
            {
                new Bin{ ID = 1, BinWeight=30 },
                new Bin{ ID = 2, BinWeight=25},
                new Bin{ ID = 3, BinWeight=10 },
                 new Bin{ ID = 4, BinWeight=20 },
                  new Bin{ ID = 5, BinWeight=15 }
            };


            int n = InitalList.Count;

            List<Bin> P1 = nextFit(InitalList, n, c);
            List<Bin> P2 = nextFit(InitalList, n, c);

            FillListAfterCrossoverP1(P1);
            FillListAfterCrossoverP2(P2);

            Random r = new Random();
            int randomindex = r.Next(1, P1.Count);

            for (int i = 1; i <= randomindex; i++)
            {
                var obj = P1.Where(x => x.ID == i).SingleOrDefault();
                Crossoverlist.Add(obj);
            }

            for (int i = randomindex+1; i <= P2.Count; i++)
            {
                var obj = P2.Where(x => x.ID == i).SingleOrDefault();
                Crossoverlist.Add(obj);
            }

            FillListAfterCrossoverFinal(Crossoverlist);

        }


        private void FillListAfterCrossoverP1(List<Bin> final)
        {

            final = final.OrderBy(x => x.ID).ToList();
          
            string anser = "";
            foreach (var item in final)
            {
                anser = anser + "      " + item.Actual_Bin;
            }

            label17.Text = anser;
        }

        private void FillListAfterCrossoverP2(List<Bin> final)
        {

            final = final.OrderBy(x => x.ID).ToList();

            string anser = "";
            foreach (var item in final)
            {
                anser = anser + "      " + item.Actual_Bin;
            }

            label15.Text = anser;
        }

        private void FillListAfterCrossoverFinal(List<Bin> final)
        {

            string anser = "";
            foreach (var item in final)
            {
                anser = anser + "      " + item.Actual_Bin;
            }

            label13.Text = anser;
        }
    }
}
