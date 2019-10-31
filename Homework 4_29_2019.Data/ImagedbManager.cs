using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework_4_29_2019.Data
{
    public class ImagedbManager
    {
        private string _connectionstring;

        public ImagedbManager(string _Connectionstring)
        {
            _connectionstring = _Connectionstring;
        }

        public int GetNumberOfLikesForImage(int id)
        {
            using (ImageContext IC = new ImageContext(_connectionstring))
            {
                return IC.Images.FirstOrDefault(i => i.id == id).LikeCount;
            }
        }
        public IEnumerable<Image> GetImages()
        {
            using (ImageContext IC = new ImageContext(_connectionstring))
            {
                List<Image> images = new List<Image>(IC.Images);
                return images;
            }
        }
        public Image GetImageById(int id)
        {
            using (ImageContext IC = new ImageContext(_connectionstring))
            {
                return IC.Images.FirstOrDefault(i => i.id == id);
            }
        }
        public void AddImage(Image image)
        {
            using (ImageContext IC = new ImageContext(_connectionstring))
            {
                IC.Images.Add(image);
                IC.SaveChanges();
            }
        }
        public void AddLikeToImage(int id)
        {
            using (ImageContext IC = new ImageContext(_connectionstring))
            {
                Image I = IC.Images.FirstOrDefault(i => i.id == id);
                I.LikeCount++;
                IC.SaveChanges();
            }
        }
    }
}
