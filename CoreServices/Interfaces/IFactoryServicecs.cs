using ServiceModel;

namespace CoreServices.Interfaces {
   public interface IFactoryServicecs
    {
        IJobService GetService(ServiceType serviceType);
    }


}
