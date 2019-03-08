using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace IPsWCF
{
    class MainClass
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            // set MONO_STRICT_MS_COMPLIANT
            if (Environment.GetEnvironmentVariable("MONO_STRICT_MS_COMPLIANT") != "yes")
            {
                Environment.SetEnvironmentVariable("MONO_STRICT_MS_COMPLIANT", "yes");
            }

            var host = new ServiceHost(typeof(IPsRepositoryWCF), new Uri("http://localhost:5001/IPsRepository.svc"));
            host.AddServiceEndpoint(typeof(IIPsRepositoryWCF), new BasicHttpBinding(), string.Empty);
            // add the MetadataBehavior to enable HttpGet
            host.Description.Behaviors.Remove<ServiceMetadataBehavior>();
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            host.Open();

            Console.WriteLine("press ENTER to close");
            Console.ReadLine();

            host.Close();
        }
    }
}
