using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;


namespace Activity_Recogniser
{
    class SVD
    

    {
       public double[][,] data = new double[20][,];
       public double[][,] M = new double[20][,];
       int r;
       int s;
       double[,] U;
       double[,] V;
        public void read() {
            
            string line;

            // Read the file and display it line by line.
            for (int j = 0; j < 5; j++)
            {
                int counter = 0;
                System.Console.WriteLine("left_hand"+j.ToString()+".txt");
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"left_hand"+j.ToString()+".txt");
                data[j] = new double[81, 10];
                while ((line = file.ReadLine()) != null)
                {
                  //  System.Console.WriteLine(line + ":" + counter.ToString());
                    string[] lines = line.Split();
                 
                    for (int i = 0; i < lines.Length; i++)
                    {

                        data[j][ counter, i] = Int32.Parse(lines[i]);

                   
                    }
                    counter++;
                }

                file.Close();
            }
            for (int j = 5; j < 10; j++)
            {
                int counter = 0;
                System.Console.WriteLine("right_hand" + (j - 5).ToString() + ".txt");
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"right_hand" + (j - 5).ToString() + ".txt");
                data[j] = new double[81, 10];
                while ((line = file.ReadLine()) != null)
                {
                    //  System.Console.WriteLine(line + ":" + counter.ToString());
                    string[] lines = line.Split();

                    for (int i = 0; i < lines.Length; i++)
                    {

                        data[j][counter, i] = Int32.Parse(lines[i]);

                    }

                    counter++;
                }

                file.Close();
            }
            for (int j = 10; j < 15; j++)
            {
                int counter = 0;
                System.Console.WriteLine("bend" + (j - 10).ToString() + ".txt");
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"bend" + (j - 10).ToString() + ".txt");
                data[j] = new double[81, 10];
                while ((line = file.ReadLine()) != null)
                {
                    //  System.Console.WriteLine(line + ":" + counter.ToString());
                    string[] lines = line.Split();

                    for (int i = 0; i < lines.Length; i++)
                    {

                        data[j][counter, i] = Int32.Parse(lines[i]);

                    }

                    counter++;
                }

                file.Close();
            }
            for (int j = 15; j < 20; j++)
            {
                int counter = 0;
                System.Console.WriteLine("both_hand" + (j - 15).ToString() + ".txt");
                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"both_hand" + (j - 15).ToString() + ".txt");
                data[j] = new double[81, 10];
                while ((line = file.ReadLine()) != null)
                {
                    //  System.Console.WriteLine(line + ":" + counter.ToString());
                    string[] lines = line.Split();

                    for (int i = 0; i < lines.Length; i++)
                    {

                        data[j][counter, i] = Int32.Parse(lines[i]);

                    }

                    counter++;
                }

                file.Close();
            }
           
         //   System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.
            System.Console.ReadLine();
     
        }
         

        public double[,] sub(double[,] a, double[,] b, int m, int n) {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                  //  
                    a[i, j] = a[i, j] - b[i, j];
                    

                }
            }
            return a;
        }
        public double[,] add(double[,] a, double[,] b, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = a[i, j] + b[i, j];

                }
            }
            return a;
        }

        public double[,] transpose(double[,] a, int m , int n){
            double[,] result = new double[n, m];

            for (int b = 0; b < m; b++)
            {
                for (int c = 0; c < n; c++)
                {

                    result[c, b] = a[b, c];
                }
            }

            return result;
        }



        public double[,] multiplication(double[,] a, double[,] b, int m, int n,int o)
        {
            double[,] result = new double[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = 0;
                    
                    for (int k = 0; k < o; k++)
                    {
                       // System.Console.WriteLine(i.ToString() + " " + j.ToString()+" "+k.ToString());
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }



            return result;
        }

        public double[,] divide(double[,] a, int m, int n, int o)
        {
            

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    
                    a[i,j] = a[i,j]/o;
                    
                   
                }
            }



            return a;
        }



       public  void preprocessing(int m , int n) {
         /*   double[,] result = new double[n,n];

            double[,] i = new double[m, m];

            for (int b = 0; b < m; b++) {
                for (int c = 0; c < m; c++)
                {

                    i[b, c] = 1.0;
                }
            }

            double[,] a = multiplication(i,matrix, m, n,m);

            for (int b = 0; b < m; b++)
            {
                for (int c = 0; c < n; c++)
                {

                    matrix[b,c]=matrix[b,c]-a[b,c];
                }
            }
            i = new double[n, m];


            result = multiplication(i, matrix, n, n, m);*/
           double[,] avg = new double[m, n];
           for (int i = 0; i < 20; i++) {
               for (int j = 0; j < m; j++) {
                   for (int k = 0; k < n; k++) {
                       avg[j, k] = avg[j,k]+ data[i][ j, k] / 20;
                   }
               }           
           }
           double[,] f = new double[m,m];
           double[,] g = new double[n,n];
           f = (multiplication(sub(data[0], avg, m, n), transpose(sub(data[0], avg, m, n), m, n), m, m, n));

           g = (multiplication(transpose(sub(data[0], avg, m, n), m, n), sub(data[0], avg, m, n), n, n, m));

      //     System.Console.WriteLine(f[m - 1, m - 1]);    
               for (int j = 1; j < 20; j++)
               {
                    f=add(f,(multiplication(sub(data[j], avg, m, n),transpose(sub(data[j], avg, m, n),m,n),m,m,n)),m,m);
                
                   g = add(g, (multiplication(transpose(sub(data[j], avg, m, n), m, n), sub(data[j], avg, m, n), n, n, m)), n, n);
                   
               }

               f = divide(f, m, m, 20);
               g = divide(g, n, n, 20);
        
            double[] eigen_value;
            double[,] eigen_vector;
             r = 5;
             s = 5;
             U = new double[m,r];
             V = new double[n, s];
        
           alglib.smatrixevd(f, m, 1, true, out eigen_value, out eigen_vector);
            for (int p = 0; p < n; p++)
            {
                System.Console.WriteLine(Convert.ToInt32(eigen_value[p]).ToString() + ":f");
            }
            for (int j = 0; j < m; j++)
            {
                for (int i = m - 1; i > m - r - 1; i--)
                 {
                    U[j,m - 1 - i] = eigen_vector[j,i];
                }
            }

            alglib.smatrixevd(g, n, 1, true, out eigen_value, out eigen_vector);
            for (int p = 0; p < n; p++)
            {
                System.Console.WriteLine(Convert.ToInt32(eigen_value[p]).ToString() + ":g");
            }
            for (int j = 0; j < n; j++)
            {
                for (int i = n - 1; i > n - s - 1; i--)
               
                {
                    V[j,n-i-1] = eigen_vector[j,i];
                }
            }
             U = transpose(U,m,r); 
            for (int j = 0; j < 20; j++)
            {
                M[j] = multiplication(multiplication(U, data[j], r, n, m), V, r, s, n);
            }

               
        }


       public double distance(double[,] a, double[,] b, int m, int n) {
           double mod1, mod2, result = 0;
           for (int i = 0; i < n; i++) {
               mod1 = 0;
               for (int j = 0; j < m; j++)
               {
                   mod1 = mod1 + (a[j, i] - b[j, i]) * (a[j, i] - b[j, i]);

               }

                   result = result + System.Math.Sqrt(mod1);
               
           }
           return result;
       }



       public string check(double[,] matrix, int m, int n)
       {


           double[,] new_matrix = new double[r, s];
         
              new_matrix = multiplication(multiplication(U,matrix, r, n, m), V, r, s, n);
              double minimum = distance(new_matrix, M[0], r, s);
              List<double> list1 = new List<double>();
              List<int> list2 = new List<int>();
              list2.Add(0);
              list1.Add(minimum);
              int label = 0 ;
           for (int i = 0; i < 20; i++) {
               if (list1.Count <= 2)
               {
                   list1.Add(distance(new_matrix, M[i], r, s));
                   list2.Add(i/5);
               }
               else if (distance(new_matrix, M[i], r, s) < list1.Max()){
                      minimum = distance(new_matrix, M[i], r, s);
                      list1[list1.IndexOf(list1.Max())] = minimum;
                      label=i;
                      list2[list1.IndexOf(list1.Max())] = i/5;
                      System.Console.WriteLine(label+" "+minimum);
              }
         
       }
           System.Console.WriteLine("--------");

           int most = (from i in list2
                       group i by i into grp
                       orderby grp.Count() descending
                       select grp.Key).First();

           if (most == 0) return "left_hand";
           else if (most == 1) return "right_hand";
           else if (most == 2) return "bend";
           else if (most == 3) return "both_hand";
           else return "can_t say";
    }
}
    }
