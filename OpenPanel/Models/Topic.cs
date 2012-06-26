using System;
using System.Collections.Generic;

namespace OpenPanel.Models
{
    public class Topic
    {
        public string Title { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Opinion> Opinions { get; set; }

        public Topic()
        {
        }
    }
}

