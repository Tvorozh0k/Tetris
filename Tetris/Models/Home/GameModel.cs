using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tetris.Models.Home
{
    public class GameModel
    {
        public int Id { get; set; }
        public DateTime GameDate { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}