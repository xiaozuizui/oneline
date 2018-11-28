using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace oneline
{
    public class Oneline
    {
        public uint width;
        public uint height;
        public State [,]States;
        public List<Point> Road;

        public Point start { set; get; }
        public Point end { set; get; }


        public Oneline(uint x, uint y)
        {
            width = x;
            height = y;
            start=new Point();
            States = new State[x,y];
            Road= new List<Point>();
            Init();
        }

        public void SetStart(uint x, uint y)
        {
            start.X = x;
            start.Y = y;
        }

        public void SetState(uint x, uint y, State state)
        {
            States[x, y] = state;
        }

        public void SetState(Point point, State state)
        {
            States[point.X, point.Y] = state;
        }

        private void Init()
        {
            for (int i = 0; i < width;i++) 
            {
                for (int j = 0; j < height;j++)
                {
                    States[i, j] = State.None;
                }
            }

        }



        public void Caculate()
        {

            if (Road.Count == 0)
            {
                //无解
                return;
            }
            Point currentPoint = Road[Road.Count-1];

            //wasd

            if (currentPoint.Y > 0)
            {
                if (States[currentPoint.X, currentPoint.Y - 1] == State.Avaliable)
                {
                    Walk(currentPoint.X,currentPoint.Y-1);
                    Caculate();
                }
            }

            if (currentPoint.X > 0)
            {
                if (States[currentPoint.X - 1, currentPoint.Y] == State.Avaliable)
                {
                    Walk(currentPoint.X-1,currentPoint.Y);
                    Caculate();
                }
            }

            if (currentPoint.Y + 1 < height)
            {
                if (States[currentPoint.X, currentPoint.Y + 1] == State.Avaliable)
                {
                    Walk(currentPoint.X, currentPoint.Y + 1);
                    Caculate();
                }
            }

            if (currentPoint.X + 1 < width)
            {
                if (States[currentPoint.X + 1, currentPoint.Y] == State.Avaliable)
                {
                    Walk(currentPoint.X+1,currentPoint.Y);
                    Caculate();
                }
                
            }

            if (!IsFinished())
            {
                Point last = Road[Road.Count-1];
                SetState(last,State.Avaliable);
                Road.Remove(last);
                
            }
            else
            {
                return;
            }


        }

        public void InitCaculate()
        {
            SetState(start,State.Passed);
          //  SetState(end,State.None);

            Road.Add(start);
        }

        private void Walk(uint x, uint y)
        {
            Point temp = new Point(x,y);
            Road.Add(temp);
            SetState(temp, State.Passed);
        }

        private bool IsFinished()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (States[i, j] == State.Avaliable)
                        return false;
                }
            }

            return true;
        }
    }

    public enum State
    {
        
        None,
        Avaliable,
        Passed
    }
    public class Point
    {
        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(uint _x, uint _y)
        {
            X = _x;
            Y = _y;
        }

        public uint X;

        public uint Y;

    }
}
