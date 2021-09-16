using Core.Model;
using Core.Model.Repositories;
using Core.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service
{
  public  interface IServiceWrapper
    {
          ISubscribeRequestService subscribeRequestService { get; }
          ISettingsService settingsService { get; }
          IEmailSender emailSender { get; }
          IProblemService ProblemService { get; }
          IHelpService helpService { get; }
          IUserService userService { get; }
   
    }
    public class ServiceWrapper : IServiceWrapper
    {
        private ISubscribeRequestService _subscribeRequestService;
        private ISettingsService _settingsService;
        private IEmailSender _emailSender;
        private IHelpService _helpService;
        private IUserService _userService;
        private IProblemService _problemsService;

        public IRepositoryWrapper _RepositoryWrapper;
        public ServiceWrapper(IRepositoryWrapper RepositoryWrappe)
        {
            _RepositoryWrapper = RepositoryWrappe;
        }
        public ISubscribeRequestService subscribeRequestService
        {
            get
            {
                if (_subscribeRequestService == null)
                {
                    _subscribeRequestService = new SubscribeRequestService(_RepositoryWrapper.SubscribeRequestRepository);
                }
                return _subscribeRequestService;
            }
        }
        public ISettingsService settingsService
        {
            get
            {
                if (_settingsService == null)
                {
                    _settingsService = new SettingsService(_RepositoryWrapper.SettingsRepository);
                }
                return _settingsService;
            }
        }

        public IEmailSender emailSender
        {
            get
            {
                if (_emailSender == null)
                {
                    _emailSender = new EmailSender();
                }
                return _emailSender;
            }
        }

        public IProblemService ProblemService
        {
            get
            {
                if (_problemsService == null)
                {
                    _problemsService = new ProblemService(_RepositoryWrapper.ProblemRepository);
                }
                return _problemsService;
            }
        }

        public IHelpService helpService
        {
            get
            {
                if (_helpService == null)
                {
                    _helpService = new HelpService(_RepositoryWrapper.HelpRepository);
                }
                return _helpService;
            }
        }

        public IUserService userService
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService(_RepositoryWrapper.UserRepository);
                }
                return _userService;
            }
        }

       

    }
}
