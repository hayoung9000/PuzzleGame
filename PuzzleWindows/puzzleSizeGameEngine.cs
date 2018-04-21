using System;

namespace PuzzleWindows
{
    internal class PuzzleGameEngine
    {
        int[] theViewIndices;
        private int puzzleSize = 16;
        public PuzzleGameEngine() { 
            //이미지 index 가져오자
            theViewIndices=new int[puzzleSize];
            for(int i = 0; i < 16; i++)
            {
                theViewIndices[i] = i;
            }

            //이미지 index 섞자
            Random rand = new Random();
            for (int n = 0; n < 10000; n++)
            {
                int i = rand.Next(0, theViewIndices.Length);
                //int j = rand.Next(0, theViewIndices.Length);
                //Swap(i, j);
                Change(i);
            }
        }

        private void Swap(int i1, int i2)
        {
            int sum =theViewIndices[i1];
            theViewIndices[i1] = theViewIndices[i2];
            theViewIndices[i2] = sum;
        }

        internal int GetViewIndex(int index)
        {
            return theViewIndices[index];
        }

        internal void Change(int touchedIndex)
        {
            //터치한 인덱스 상하좌우에 빈 인덱스 있다면 교환
            if(GetEmptyIndex()/4==touchedIndex/4&&
                (GetEmptyIndex() == touchedIndex - 1
                || GetEmptyIndex() == touchedIndex + 1)
                || GetEmptyIndex() == touchedIndex - 4
                || GetEmptyIndex() == touchedIndex + 4)
            {
                Swap(GetEmptyIndex(), touchedIndex);
            }
        }

        private int GetEmptyIndex()
        {
            for (int i = 0; i < puzzleSize; i++)
            {
                if (theViewIndices[i] == puzzleSize - 1) return i;
            }
            return -1;
        }

        internal bool isEnd()
        {
            int cnt = 0;
           for(int i = 0; i < puzzleSize; i++)
            {
                if (theViewIndices[i] == i) cnt++;
            }
            return cnt == puzzleSize;
        }
    }
}