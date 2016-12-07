using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3
{
   class Matrix
        {

            private int dim = 0;
            protected double[,] mtx;
            private double[,] mtxL;
            private double[,] mtxU;
            protected double[] b;
            protected double[] sln;

            public Matrix copy(Matrix a1, Matrix a2)
            {

                    for(int i = 0; i < dim; i++) {
                        for (int j = 0; j < dim; j++)
                        {
                            a1.mtx[i, j] = a2.mtx[i, j];
                        }

                    }
                    return a1;
            }


            public Matrix matL()
            {

                Matrix tempL = new Matrix(dim);
                copy(tempL, this);
                return tempL;

            }


            public double getRHS(int index)
            {
                return b[index];
            }


            
            public void rhs(double x, int index)
            {
                b[index] = x;
            }

            public void augRHS(double x, int index)
            {
                b[index] = b[index] + x;
            }

            
            public void multRHS(int index, double x)
            {
               b[index] *= x*x;
            }




            public double ij(int i, int j)
            {
                return mtx[i,j]; 
            }

            public void ijSet(double x, int i, int j)
            {
                mtx[i, j]=x;
            }

            
            
            public double ijL(int i, int j)
            {
                return mtxL[i, j];
            }

            public double ijU(int i, int j)
            {
                return mtxU[i, j];
            }

            

            public int Ndim
            {
                get { return dim; }
            }

            
            
            public void makeP1D() {
                int i, j;
                try
                        {
                                if(dim == 0)          throw new DivideByZeroException();
                            }
                                catch (DivideByZeroException e)
                        {
                                    Console.WriteLine("Matrix dimensions should be initialized ");
                        }


                mtx[0, 0] = -2.0;
                mtx[dim - 1, dim - 1] = -2.0;
                for(i = 0; i < dim-1; i++) {

                                mtx[i, i] = -2.0;
                                mtx[i + 1, i] = 1.0;
                                mtx[i, i+1] = 1.0;

                     }

        }




            
            
            public void decompLU() 
            {
                int i;

                double[] bb = Enumerable.Repeat(0.0,dim).ToArray();
                double[] dd = Enumerable.Repeat(0.0, dim).ToArray();

                bb[0] = 0.0;
                dd[0] = mtx[0, 0];
                
   
                
                
                for (i = 1; i < dim; i++)
                {
                    bb[i] = 1.0 / dd[i - 1];
                    dd[i] = mtx[i, i] - bb[i];
//                    Console.WriteLine("MtxU is {0:0.##}, {1:0.##},{2:0.##}", dd[i], i,bb[i]);
                }

                


                for (i = 0; i < dim-1; i++)
                {
                    mtxL[i, i] = 1.0;
                    mtxL[i+1, i] = bb[i+1];
                    mtxU[i, i] = dd[i];
                    mtxU[i, i+1] = 1.0;
                    

                }


                mtxL[dim - 1, dim - 1] = 1.0;
                mtxU[dim - 1, dim - 1] = dd[dim-1];
//                mtxU[dim - 1, dim - 1] = 10.0;
            }


            public static Matrix operator +(Matrix a1, Matrix a2)
            {

                Matrix temp = new Matrix(a1.dim);
                for (int i = 0; i < a1.dim; i++)
                {
                    for (int j = 0; j < a1.dim; j++)
                    {
                        temp.mtx[i, j] = a1.mtx[i, j] + a2.mtx[i, j];

                    }


                }

                return temp;

            }














            public static Matrix operator *(Matrix a1, Matrix a2)
            {

                double sum;
                Matrix temp = new Matrix(a1.dim);
                for (int i = 0; i < a1.dim; i++)
                {
                    for (int j = 0; j < a1.dim; j++)
                    {
                        sum = 0;
                        for (int k = 0; k < a1.dim; k++) 
                        {

                            sum = sum + a1.mtx[i, k] * a2.mtx[k, j];
                        }

                        temp.mtx[i, j] = sum;
                    }


                }

                return temp;

            }



            public static double [] operator ~(Matrix a1)
            {
                int n = a1.Ndim -1 ;
                double[] x = new double[n + 1];
                double[] y = new double[n + 1];
                double suml = 0;
                double sumu = 0;
                double lij = 0;

                for (int i = 0; i <= n; i++)
                {
                    suml = 0;
                    for (int j = 0; j <= i - 1; j++)
                    {
                        if (i == j)
                        {
                            lij = 1;
                        }
                        else
                        {
                            lij = a1.mtxL[i,j];
                        }
                        suml = suml + (lij * y[j]);
                    }
                    y[i] = a1.b[i] - suml;
                }
                //Solve for x by using back substitution
                for (int i = n; i >= 0; i--)
                {
                    sumu = 0;
                    for (int j = i + 1; j <= n; j++)
                    {
                        sumu = sumu + (a1.mtxU[i,j] * x[j]);
                    }
                    x[i] = (y[i] - sumu) / a1.mtxU[i,i];
                    a1.sln[i] = x[i];
                }

                return x;

            }
 


            
            public static Matrix operator !(Matrix a1)
            {

                double sum;
                Matrix temp = new Matrix(a1.dim);
                for (int i = 0; i < a1.dim; i++)
                {
                    for (int j = 0; j < a1.dim; j++)
                    {
                        sum = 0;
                        for (int k = 0; k < a1.dim; k++)
                        {

                            sum = sum + a1.mtxL[i, k] * a1.mtxU[k, j];
                        }

                        temp.mtx[i, j] = sum;
                    }


                }

                return temp;

            }

            public  Matrix(int ndim)
            {
                mtx = new double[ndim, ndim];
                mtxL = new double[ndim, ndim];
                mtxU = new double[ndim, ndim];
                b = new double[ndim];
                sln = new double[ndim];
                for (int i = 0; i < ndim; i++)
                {
                    for (int j = 0; j < ndim; j++)
                    {
                        mtx[i, j] = mtxL[i, j] = mtxU[i, j] = 0.0;
                    }

                    b[i] = 0.0;
                }

                    dim = ndim;
            }
        }
    }
