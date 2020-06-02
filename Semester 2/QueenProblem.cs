using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace Cursovaya
{
    class QueensProblem
    {
        private int Size { get; set; }
        private int[] positions;
        private List<int[]> saved;

        public void Init(int size)
        {
            this.Size = size;
            positions = new int[size];
            saved = new List<int[]>();
        }

        private void Save()
        {
            saved.Add((int[])positions.Clone());
        }

        private bool Step()
        {
            int s = Size;
            for (int i = 1; i < s; i++)// пробежка по столбцам
            {
                int pos = positions[i];
                while (true)
                {
                    bool found = false;
                    for (int j = 0; j < i; j++) // пробежка уже по поставленным ферзям в столбцах
                    {
                        int epos = positions[j];
                        if (epos == pos || pos - i == epos - j || pos + i == epos + j)// проверка мира между уже установленными ферзями
                        {
                            if (++pos >= s)
                            {
                                if (!Next(i)) return false;              
                                while (++i < s) positions[i] = 0;
                                return true;
                            }
                            positions[i] = pos;
                            found = true;
                            break;
                        }
                    }
                    if (!found) break;
                }
            }
            Save();
            return Next(s - 1);
        }

        private bool Next(int pos)
        {
            int s = Size;
            if (++positions[pos] < s) return true;
            
            while (pos > 0) 
            {
                positions[pos] = 0;
                if (++positions[--pos] < s) return true; 
            } 
            return false;
        }

        public int Solve()
        {
            while (Step()) ;            
            return saved.Count;
        }

        public List<int[]> GetResults()
        {
            return saved;
        }

     

    }
    internal static class Sound
    {
        [DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern bool PlaySound(string szSound, System.IntPtr hMod, SoundFlags flags);

        [System.Flags]
        public enum SoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }
    }



}
    

