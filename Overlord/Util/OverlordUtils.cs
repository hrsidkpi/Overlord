using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlord.Util
{
	public static class OverlordUtils
	{

		public static T GetResource<T>(string name)
		{
			return (T) App.Current.Resources[name];
		}

	}
}
