namespace NBA.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Teams
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string TeamName { get; set; }
        public string Conference { get; set; }
    }
    public enum Team
    {
        FreeAgent,
        AtlantaHawks,
        BostonCeltics,
        BrooklynNets,
        CharlotteHornets,
        ChicagoBulls,
        ClevelandCavaliers,
        DallasMavericks,
        DenverNuggets,
        DetroitPistons,
        GoldenStateWarriors,
        HoustonRockets,
        IndianaPacers,
        LosAngelesClippers,
        LosAngelesLakers,
        MemphisGrizzlies,
        MiamiHeat,
        MilwaukeeBucks,
        MinnesotaTimberwolves,
        NewOrleansPelicans,
        NewYorkKnicks,
        OklahomaCityThunder,
        OrlandoMagic,
        Philadelphia76ers,
        PhoenixSuns,
        PortlandTrailBlazers,
        SacramentoKings,
        SanAntonioSpurs,
        TorontoRaptors,
        UtahJazz,
        WashingtonWizards,
        Error
    }
}
