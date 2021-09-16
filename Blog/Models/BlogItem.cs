using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogItem
    {
        [Key]
        public int blogId { get; set; }

        [Required(ErrorMessage ="Title is Required")]
        [MaxLength(150)]
        public string blogTitle { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(300)]
        public string blogDescription { get; set; }
        public string blogUrlImage { get; set; }
        
        [NotMapped]
        [DisplayName("UploadFile")]
        public IFormFile UploadedImage { get; set; }
    }

    public enum BlogType
    {
        local=1,
        remota=2,
        remotaPlus=3
    }
}
