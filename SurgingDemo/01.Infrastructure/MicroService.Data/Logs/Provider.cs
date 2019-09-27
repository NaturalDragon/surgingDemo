using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Data.Log
{
    public class Provider
    {
        private static readonly string LOG_CONFIG_PATH = string.Format(@"{0}\log4net.config", AppDomain.CurrentDomain.BaseDirectory);
        private static ILoggerRepository _loggerRepository;
        private static readonly object Lock = new object();

        public static ILog GetLogger(string repository, string name)
        {
            try
            {
                if (LogManager.GetAllRepositories().All(m => m.Name != repository))
                {
                    RegistRepository(repository);
                }
                else
                {
                    _loggerRepository = LogManager.GetRepository(repository);
                }
                return log4net.LogManager.GetLogger(_loggerRepository.Name, name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static ILoggerRepository RegistRepository(string repository)
        {
            _loggerRepository = LogManager.CreateRepository(repository);
            log4net.Config.XmlConfigurator.Configure(_loggerRepository, new System.IO.FileInfo(LOG_CONFIG_PATH));
            return _loggerRepository;
        }
    }
}
