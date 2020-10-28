using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class LifeFormData
{
    protected WorldData instance;

    [System.NonSerialized]
    public GameTile occupyingTile;

    public LifeForm Species { get; set; }

    private int maximumAge;

    public int currentAge;
    public int timeSinceLastReproduction;
    public int currentHealth;

    public MaturityStage CurrentMaturity()
    {
        if (Dead())
        {
            return MaturityStage.dead;
        }
        else
        {
            return currentAge >= Species.ageToMaturity ? MaturityStage.mature : MaturityStage.immature;
        }
    }

    public int CurrentReproductionTime()
    {
        int baseLevel = Species.baseReproductionTime;
        int modifier = Species.baseReproductionTime * (1 - currentHealth / Species.baseHealth);
        return baseLevel + modifier;
    }

    public bool ReadyToReproduce()
    {
        return CurrentReproductionTime() <= timeSinceLastReproduction;
    }

    public LifeFormData(LifeForm species, GameTile tile)
    {
        instance = WorldData.GetInstance();
        this.occupyingTile = tile;
        this.Species = species;
        currentAge = 0;
        currentHealth = species.baseHealth;
        timeSinceLastReproduction = 0;
        maximumAge = Random.Range(species.minimumLifespan, species.maximumLifespan + 1);
    }

    public void UpdateLifeForm()
    {
        //advance age
        Advance();

        //handle health
        HandleHealth();

        //handle reproduction
        HandleReproducton();

        //handle death
        HandleDeath();
    }

    public void HandleDeath()
    {
        if (Dead())
        {
            ImplHandleDeath();
        }
    }

    protected abstract void ImplHandleDeath();

    public void Advance()
    {
        currentAge++;
        timeSinceLastReproduction++;
    }

    private void HandleReproducton()
    {
        if (ReadyToReproduce())
        {
            GameTile tile = occupyingTile;
            List<Coords> neighbours = tile.neighbouringCoords;
            int randomNumber = Random.Range(0, neighbours.Count);
            Coords pickedCoord = neighbours[randomNumber];
            ImplReproduce(pickedCoord);
            ResetReproduction();
        }
    }

    protected abstract void ImplReproduce(Coords pickedCoord);

    private void HandleHealth()
    {
        //handle liked species
        HandleLikedSpecies();

        //handle disliked species
        HandleDislikedSpecies();
    }

    private void HandleLikedSpecies()
    {
        bool likedSpeciesFound = false;

        if(Species.likedSpecies.Count() > 0)
        {
            GameTile tile = this.occupyingTile;
            List<Coords> neighbours = occupyingTile.neighbouringCoords;
            foreach (var neighbour in neighbours)
            {
                GameTile neighbourTile = instance.GetTileByCoord(neighbour);
                foreach (var item in neighbourTile.GetAllLifeForms())
                {
                    if (Species.likedSpecies.Contains(item.Species))
                    {
                        likedSpeciesFound = true;
                        break;
                    }
                }
                if (likedSpeciesFound)
                {
                    break;
                }
            }
        }
        else
        {
            likedSpeciesFound = true;
        }

        if (!likedSpeciesFound)
        {
            Damage(1);
        }
    }

    private void HandleDislikedSpecies()
    {
        GameTile tile = this.occupyingTile;
        List<Coords> neighbours = occupyingTile.neighbouringCoords;
        bool dislikedSpeciesFound = false;
        foreach (var neighbour in neighbours)
        {
            GameTile neighbourTile = instance.GetTileByCoord(neighbour);
            foreach (var item in neighbourTile.GetAllLifeForms())
            {
                if (Species.dislikedSpecies.Contains(item.Species))
                {
                    dislikedSpeciesFound = true;
                    break;
                }
            }
            if (dislikedSpeciesFound)
            {
                break;
            }
        }
        Damage(dislikedSpeciesFound? 1 : 0);
    }

    public bool Dead()
    {
        return currentAge > maximumAge || currentHealth <= 0;
    }

    public void Damage(int healthDamage)
    {
        currentHealth -= healthDamage;
    }

    public void ResetReproduction()
    {
        timeSinceLastReproduction = 0;
    }
}
