using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace eduNICA.Resources.Model
{
    public partial class Detallematricula
    {
        public int Id { get; set; }
        public int AsignaturasId { get; set; }
        public int MatriculasId { get; set; }
    }
}