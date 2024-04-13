namespace WebApp.Models;
public class Class{
    public string ClassId { get; set; } = null!;
    public string ClassName { get; set; } = null!;
    public override bool Equals(object? obj){
        if(obj != null){
            if(obj is Class other){
                return ClassId.Equals(other.ClassId);
            }
        }
        return false;
    }

    public override int GetHashCode(){
        return ClassId.GetHashCode();
    }
}