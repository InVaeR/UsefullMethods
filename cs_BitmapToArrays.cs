/*
Возвращает массив типа byte[3,w,h] где каждый элемент по индексу:
[0,x,y] - это значение  красной  составляющей пикселя по координатам x и y;
[1,x,y] - это значение   синей   составляющей пикселя по координатам x и y;
[2,x,y] - это значение  зеленой  составляющей пикселя по координатам x и y;
*/

public static byte[,,] BitmapToByteRGB(Bitmap bmp)
{
        int width = bmp.Width,
        height = bmp.Height;
        
        byte[,,] result = new byte[3, height, width];
        for (int y = 0; y < height; ++y)
        {
                for (int x = 0; x < width; ++x)
                {
                        Color color = bmp.GetPixel(x, y);
                        result[0, y, x] = color.R;
                        result[1, y, x] = color.G;
                        result[2, y, x] = color.B;
                }
        }
        return result;
}
        
/*
Ускоренный метод с использованием указателей
Возвращает массив типа byte[3,w,h] где каждый элемент по индексу:
[0,x,y] - это значение  красной  составляющей пикселя по координатам x и y;
[1,x,y] - это значение   синей   составляющей пикселя по координатам x и y;
[2,x,y] - это значение  зеленой  составляющей пикселя по координатам x и y;
*/
public unsafe static byte[,,] BitmapToByteRGBQuick(Bitmap bmp)
{
    int width = bmp.Width,
        height = bmp.Height;
    byte[,,] res = new byte[3, height, width];
    BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
        PixelFormat.Format24bppRgb);
    try
    {
        byte* curpos;
        for (int h = 0; h < height; h++)
        {
            curpos = ((byte*)bd.Scan0) + h * bd.Stride;
            for (int w = 0; w < width; w++)
            {
                res[2, h, w] = *(curpos++);
                res[1, h, w] = *(curpos++);
                res[0, h, w] = *(curpos++);
            }
        }
    }
    finally
    {
        bmp.UnlockBits(bd);
    }
    return res;
}
