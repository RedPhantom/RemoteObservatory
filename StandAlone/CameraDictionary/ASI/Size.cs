namespace ZWOptical.ASISDK.ObjectModel
{
    public class Size
    {
        public int Width;
        public int Height;

        public Size(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public Size()
        {
            Width = 0;
            Height = 0;
        }
    }
}