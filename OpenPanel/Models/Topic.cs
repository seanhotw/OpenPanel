using System;
using System.Collections.Generic;

namespace OpenPanel.Models
{
    public class Topic
    {
        public string Question { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Answer> Answers { get; set; }

        public Topic()
        {
        }
    }
}

