using System.Collections.Generic;

namespace Pantheon.Core.Meta.Greek
{
    public static class GreekRelics
    {
        public static Relic AresFirstBlood()
        {
            return new Relic("Ares' First Blood", "Your first Attack card each turn deals 3 additional damage.");
        }

        public static Relic HermesWingedSandals()
        {
            return new Relic("Hermes' Winged Sandals", "Your first card played each turn costs 1 less Energy (minimum 1).");
        }

        public static Relic NyxsEmbrace()
        {
            return new Relic("Nyx's Embrace", "At the start of each combat, gain 1 Strength. If you have Volley, gain 1 Volley instead.");
        }

        public static Relic MedusasGlare()
        {
            return new Relic("Medusa's Glare", "Whenever you apply Exposed, apply 1 additional stack.");
        }

        public static Relic StyxsBlessing()
        {
            return new Relic("Styx's Blessing", "The first time you would take damage each combat, gain 5 Block first.");
        }

        public static Relic ErinyesVengeance()
        {
            return new Relic("Erinyes' Vengeance", "Whenever an enemy dies, deal 2 damage to a random other enemy.");
        }

        public static Relic AphroditesBathwater()
        {
            return new Relic("Aphrodite's Bathwater", "Curse cards do nothing when played and instead grant a small random bonus (Strength, Block, or Energy) for the rest of the turn.");
        }

        public static Relic SisyphussGymMembership()
        {
            return new Relic("Sisyphus's Gym Membership", "Gain 1 Strength at the start of each turn. Lose all Strength at the start of each combat.");
        }

        public static Relic MidassCreditCard()
        {
            return new Relic("Midas's Credit Card", "Enemies drop additional Divine Essence when defeated. Cards cost 1 more Energy after the first each turn.");
        }

        public static Relic ZeussGroupProject()
        {
            return new Relic("Zeus's Group Project", "The first card you play each turn triggers its effects twice.");
        }

        public static Relic PandorasGroupChat()
        {
            return new Relic("Pandora's Group Chat", "When picked up, apply 2 random debuffs to yourself for your next combat only. Afterward, permanently gain a small bonus for the rest of the run.");
        }

        public static Relic HadesBulkDiscount()
        {
            return new Relic("Hades' Bulk Discount", "Removing cards (including Curses) at Hermes' Exchange costs less Divine Essence.");
        }

        public static Relic NarcissussFrontFacingCamera()
        {
            return new Relic("Narcissus's Front-Facing Camera", "Whenever you play a Skill card, gain 1 Block.");
        }

        public static Relic TalossCore()
        {
            return new Relic("Talos's Core", "Your Attack cards deal 2 additional damage to enemies below 50% HP.");
        }

        public static IEnumerable<Relic> RegularPool()
        {
            yield return AresFirstBlood();
            yield return HermesWingedSandals();
            yield return NyxsEmbrace();
            yield return MedusasGlare();
            yield return StyxsBlessing();
            yield return ErinyesVengeance();
            yield return AphroditesBathwater();
            yield return SisyphussGymMembership();
            yield return MidassCreditCard();
            yield return ZeussGroupProject();
            yield return PandorasGroupChat();
            yield return HadesBulkDiscount();
            yield return NarcissussFrontFacingCamera();
        }
    }
}
