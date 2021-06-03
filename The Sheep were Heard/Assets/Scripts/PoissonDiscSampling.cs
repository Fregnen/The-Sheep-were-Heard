//------------------------- Made by Sebastian Lague -------------------------
// Source code: https://github.com/SebLague/Poisson-Disc-Sampling
// Purpose: spawn the sheeps with Poisson Disc Sampling method 
// edited by: Linnea
// --------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling {

    // List with vector2 which will generate the points of spawning
	public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection = 30) {
		
        // Calculate the size of the grid (calculated from r^2 = s^2 + s^2)
        float cellSize = radius/Mathf.Sqrt(2);

        // 2D array for the grid
		int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x/cellSize), Mathf.CeilToInt(sampleRegionSize.y/cellSize)];

        // Holds the points that is created
		List<Vector2> points = new List<Vector2>();
        // Holds the spawn point (if point in points does not fit as a spawn point, remove it)
		List<Vector2> spawnPoints = new List<Vector2>();

        // First point will be set in the middle
		spawnPoints.Add(sampleRegionSize/2);
        // Pick a random spawn point, see if it fits
		while (spawnPoints.Count > 0) {
			int spawnIndex = Random.Range(0,spawnPoints.Count);
			Vector2 spawnCentre = spawnPoints[spawnIndex];
			bool candidateAccepted = false;

			for (int i = 0; i < numSamplesBeforeRejection; i++)
			{
				float angle = Random.value * Mathf.PI * 2;
				Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2*radius);
				if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid)) {
					points.Add(candidate);
					spawnPoints.Add(candidate);
					grid[(int)(candidate.x/cellSize),(int)(candidate.y/cellSize)] = points.Count;
					candidateAccepted = true;
					break;
				}
			}
			if (!candidateAccepted) {
				spawnPoints.RemoveAt(spawnIndex);
			}

		}

		return points;
	}

	static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid) {
		if (candidate.x >=0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y) {
			int cellX = (int)(candidate.x/cellSize);
			int cellY = (int)(candidate.y/cellSize);
			int searchStartX = Mathf.Max(0,cellX -2);
			int searchEndX = Mathf.Min(cellX+2,grid.GetLength(0)-1);
			int searchStartY = Mathf.Max(0,cellY -2);
			int searchEndY = Mathf.Min(cellY+2,grid.GetLength(1)-1);

			for (int x = searchStartX; x <= searchEndX; x++) {
				for (int y = searchStartY; y <= searchEndY; y++) {
					int pointIndex = grid[x,y]-1;
					if (pointIndex != -1) {
						float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
						if (sqrDst < radius*radius) {
							return false;
						}
					}
				}
			}
			return true;
		}
		return false;
	}
}