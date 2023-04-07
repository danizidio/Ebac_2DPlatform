public interface ICanBeDamaged
{
    void SufferDamage(int damage);

}

public interface IShowDamage
{
    void ShowText(string txt, UnityEngine.Color txtColor, float damage);
}
