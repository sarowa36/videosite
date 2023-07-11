using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsLayer.Math;

namespace ToolsLayer.FileManagement
{
    public static class ImageManagement
    {
        public static void Resize(this SixLabors.ImageSharp.Image img)
        {
            const int maxwidth = 1920;
            const int maxheight = 1080;
            if (img.Width > maxwidth || img.Height > maxheight)
            {
                int gcd = 1;
                int gcdwidth = img.Width;
                do
                {
                    gcd = GCD.Calculate(gcdwidth, img.Height);
                    gcdwidth--;
                } while (gcd == 1 && gcdwidth != 0);
                int verticalRatio = img.Height / gcd;
                int horizontalRatio = img.Width / gcd;
                int maxgcd = GCD.Calculate(maxheight, maxwidth);
                if (gcd > 1)
                {
                    if (img.Width > maxwidth)
                    {
                        int destwidth = 0;
                        int destheight = 0;
                        for (int i = horizontalRatio; i < maxwidth; i += horizontalRatio)
                        {
                            destheight += verticalRatio;
                            destwidth += horizontalRatio;
                        }
                        img.Mutate(x => x.Resize(destwidth, destheight));
                    }
                    if (img.Height > maxheight)
                    {
                        int destwidth = 0;
                        int destheight = 0;
                        for (int i = verticalRatio; i < maxheight; i += verticalRatio)
                        {
                            destheight += verticalRatio;
                            destwidth += horizontalRatio;
                        }
                        img.Mutate(x => x.Resize(destwidth, destheight));
                    }
                }
            }
        }
    }
}
