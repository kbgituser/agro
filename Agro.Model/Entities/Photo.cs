using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Entities
{
    public class Photo
    {        
        public int Id { get; set; }
        [Display(Name = "Путь")]
        public string Path { get; set; }
        [Display(Name = "Загружено")]
        public bool IsPhotoUploaded(string path)
        {
            return File.Exists(path + @"\" + Path);
        }
    }
}
