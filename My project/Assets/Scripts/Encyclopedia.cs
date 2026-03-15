using TMPro;
using UnityEngine;

public class Encyclopedia : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textBox;

    public void AntibodyInfo()
    {
        textBox.text = "Antibodies (The Targeted Missiles): These are the weapons produced by the " +
                       "B Cells. They are Y-shaped proteins that circulate in your blood. " +
                       "When they find their specific target (an antigen, like a piece of a virus), " +
                       "they latch onto it. This either neutralizes the virus directly " +
                       "(preventing it from entering your cells) or \"tags\" it like a flashing neon sign " +
                       "so Macrophages know to eat it.";
    }

    public void MacrophageInfo()
    {
        textBox.text = "Macrophages (The \"Eaters\" & First Responders): " +
                       "The word macrophage literally translates to \"big eater.\" " +
                       "These are large white blood cells that patrol your body. " +
                       "When they find something that doesn't belong—like a virus, " +
                       "bacteria, or dead cellular debris—they swallow it whole and digest it. " +
                       "They also release chemical signals to call for backup.";
    }
    
    public void KillerTInfo()
    {
        textBox.text = "Killer T Cells (The Hand-to-Hand Combatants): Also known as Cytotoxic T cells. " +
                       "While antibodies attack viruses floating outside your cells, " +
                       "Killer T Cells deal with the ones that have already broken in. " +
                       "They scan your body's cells, and if they find a cell that has been infected " +
                       "by a virus (or has turned cancerous), they latch onto it and inject toxic " +
                       "proteins that cause the infected cell to self-destruct, stopping the virus from multiplying.";
    }
    
    public void BCellInfo()
    {
        textBox.text = "B Cells (The Weapons Factories): These are specialized white blood cells " +
                       "that act like biological intelligence centers. When they encounter a specific " +
                       "invader, they learn its shape and start manufacturing customized weapons designed " +
                       "specifically to lock onto that exact threat.";
    }
    
    public void CovidInfo()
    {
        textBox.text = "COVID-19 (SARS-CoV-2): This virus primarily targets the respiratory system. " +
                       "Its spike proteins act like a key that perfectly fits a \"lock\" " +
                       "(the ACE2 receptor) found on the outside of human cells, particularly " +
                       "in the lungs. Once inside, it hijacks the cell to make copies of itself. " +
                       "In severe cases, your immune system overreacts (a \"cytokine storm\"), " +
                       "causing severe inflammation and fluid buildup in the lungs, making it hard to breathe.";
    }
    
    public void SmallpoxInfo()
    {
        textBox.text = "Smallpox (Variola virus): Fortunately, this virus " +
                       "has been eradicated globally through vaccination. " +
                       "It entered through the respiratory tract, multiplied in the lymph nodes, " +
                       "and then flooded the bloodstream, moving rapidly to the skin and internal " +
                       "organs. It caused high fevers and its signature, severe fluid-filled blisters across the body.";
    }
    
    public void EbolaInfo()
    {
        textBox.text = "Ebola: This virus is particularly brutal because it directly attacks the immune system first. " +
                       "It infects Macrophages and other early-responder cells, disabling the body's alarm system. " +
                       "It then spreads to the liver and blood vessels. It causes the blood vessels to leak and prevents " +
                       "blood from clotting properly, leading to severe internal and external bleeding (hemorrhagic fever).";
    }
    
    public void RabbiesInfo()
    {
        textBox.text = "Rabies: This is a neurotropic virus, meaning it targets the nervous system. " +
                       "After entering the body (usually through an animal bite), it hides from the " +
                       "immune system inside the muscle tissue for a while. Then, it latches onto peripheral " +
                       "nerves and travels up the \"wiring\" of your body directly into the brain. Once there, " +
                       "it causes fatal inflammation (encephalitis) and alters behavior before eventually causing death.";
    }
    
    public void CancerInfo()
    {
        textBox.text = "Cancer: Cancer is a disease of our own cells, not an outside invader. " +
                       "It happens when a normal cell's DNA mutates, causing it to divide uncontrollably " +
                       "and refuse to die when it should. The tricky part about cancer is that because it " +
                       "is made of your own tissue, your immune system (like Killer T Cells) often struggles to " +
                       "recognize it as a threat, allowing the tumor to grow and spread.";
    }
    
    public void BacteriophageInfo()
    {
        textBox.text = "Bacteriophage (Phages): They look a bit like alien lunar landers, and they exclusively " +
                       "target and kill bacteria. They land on a bacterium, inject their DNA into it, and " +
                       "force the bacteria to produce more phages until the bacterium explodes. Scientists are currently " +
                       "studying them as a potential treatment for bacterial infections that have become resistant to standard antibiotics.";
    }
}
