using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3
{
    class PoissonSolvers1D : Matrix
    {
        public static double pi = 4.0 * Math.Atan(1.0);
        private int nX;
        private Matrix p1D;
        private double p0;
        private double p1;
        private double dx;

        public void setRHS(int index, double val)
        {
            p1D.rhs(val, index);
            p1D.multRHS(index, dx);
        }



        public double getB(int index)
        {
            return p1D.getRHS(index);

        }

        public double[] Solve()
        {

            double[] zz = new double[nX];
            zz = ~p1D;
            return zz;

        }



        public void augB()
        {

            p1D.augRHS(p0, 0);
            p1D.augRHS(p1, nX - 1);
        }



        public double dh
        {

            get { return this.dx; }
            set { this.dx = value; }
        }



        public double initPoint
        {

            get { return this.p0; }
            set { this.p0 = value; }
        }

        public double endPoint
        {

            get { return this.p1; }
            set { this.p1 = value; }
        }



        public int nDim
        {
            get { return this.nX; }

        }



        public Matrix chckMult()
        {

            Matrix temp = new Matrix(this.Ndim);

            temp = !p1D;
            return temp;
        }


        public PoissonSolvers1D(int nD)
            : base(nD)
        {
            p1D = new Matrix(nD);
            nX = nD;
            p1D.makeP1D();
            p1D.decompLU();


        }



        public PoissonSolvers1D(int nD, int nZ)
            : base(nD)
        {

            try
            {
                throw new DivideByZeroException();
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Matrix can only be a squared one. Use constructor with only one parameter ");
            }

        }

        public double getElement(string type, int i, int j)
        {

            if (type.Equals("L"))
            {
                return p1D.ijL(i, j);
            }
            else if (type.Equals("U"))
            {
                return p1D.ijU(i, j);
            }
            else if (type.Equals("M"))
            {
                return p1D.ij(i, j);
            }

            return 0.0;

        }

        public double getElement(int i, int j)
        {

            return p1D.ij(i, j);
        }

        public void setElement(double x, int i, int j)
        {

            p1D.ijSet(x, i, j);
        }


        public static PoissonSolvers1D operator +(PoissonSolvers1D a1, PoissonSolvers1D a2)
        {
            double temp1, temp2, sum;
            PoissonSolvers1D temp = new PoissonSolvers1D(a1.nDim);
            for (int i = 0; i < a1.nDim; i++)
            {
                for (int j = 0; j < a1.nDim; j++)
                {
                    temp1 = a1.getElement(i, j);
                    temp2 = a2.getElement(i, j);
                    sum = temp1 + temp2;
                    temp.setElement(sum, i, j);

                }


            }

            return temp;

        }



    }

}
