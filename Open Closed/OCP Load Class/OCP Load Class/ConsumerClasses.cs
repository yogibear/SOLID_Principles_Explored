using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCP_Load_Class
{
	
	public class TeamsConsumer : ITopicConsumer
	{
		public string GetName() => "teamTopic";

		public void RunConsumer()
		{
			Console.WriteLine("TEAM");
		}
	}

	public class ProductsConsumer : ITopicConsumer
	{
		public string GetName() => "productTopic";

		public void RunConsumer()
		{
			// consume and process topciName
			Console.WriteLine("PRODUCT");
		}
	}

	public class UsersConsumer : ITopicConsumer
	{
		public string GetName() => "userTopic";

		public void RunConsumer()
		{
			new ConsumeUserTopic().ConsumeUserFeed();
		}
	}

	public class ConsumeUserTopic
	{
		public void ConsumeUserFeed()
		{
			Console.WriteLine("USER");
		}
	}
}
