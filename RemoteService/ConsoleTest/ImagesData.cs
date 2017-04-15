namespace ConsoleTest
{
    class ImagesData
    {
        public string savePath;
        public int slidesCount;
        public int imageBufferLength;

        public ImagesData(string savePath, int slidesCount, int imageBufferLength)
        {
            this.savePath = savePath;
            this.slidesCount = slidesCount;
            this.imageBufferLength = imageBufferLength;
        }
    }
}
