namespace OneCloud
{

abstract class IResource{
               string Name;
        ResourceType ResourceType;
        Region Region;
        CloudProvider CloudProvider;

        public abstract int Create();
        public abstract int Delete();
        public abstract int Modify();
    }
}
