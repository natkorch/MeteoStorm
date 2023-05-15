using MediatR;

namespace MeteoStorm.InfoHub.MediatR
{
  public abstract class ModelCommand<T>: IRequest<T>
  {
    public T Model { get; }

    public ModelCommand(T model)
    {
      Model = model;
    }
  }
}
