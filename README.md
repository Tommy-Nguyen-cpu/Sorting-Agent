# Sorting-Agent
Trained a reinforcement learning agent to sort items based on item type (trash or recycleable item) using Unity's ML-Agents package.

# Requirements
The project was built on Unity 2019 version. In order for the project to run successfully, you must have a 2019 version of Unity. However, I believe it should still work for newer version of Unity as long as you bring over the assets (Scripts, prefabs, etc) to the newer version of Unity.

## Running
To access this GitHub repository, follow these steps:

1. Open Unity Hub.
2. Click on "Open" and navigate to the folder containing the GitHub repository.
3. Within the repository folder, locate the "ML Agents Test" folder.
4. Select the "ML Agents Test" folder and click the "Open" button in the mini window.

Once you are there, open an anaconda terminal and follow these instructions to install the ml-agents package:
2. Install Anaconda

3. conda create -n mlagents python=3.10.12 && conda activate mlagents

3a. https://git-scm.com/download/win

4. git clone https://github.com/Unity-Technologies/ml-agents.git

5. pip3 install torch~=1.13.1 -f https://download.pytorch.org/whl/torch_stable.html

6.cd /path/to/ml-agents
7. python -m pip install ./ml-agents-envs
8. python -m pip install ./ml-agents

9. Run the following to verify mlagents-learn works: mlagents-learn --help

In an Anaconda terminal using the environment we just created, run the following command to start training, then click the play button in the Unity editor:
mlagents-learn {PATH\TO\YAML\CONFIG\FILE} --run-id={ID OF CONFIG FILE}

An example yaml file is provided in "ML Agents Test/Assets/Yaml Files/".
## Notes
If you encounter error messages indicating that "XboxSeriesOne" and "Ps5" do not exist, follow these steps to resolve the issue:

1. Double-click on the error message to navigate to the source of the error.
2. Locate the block of code responsible for the error. It should be within `#if UNITY_2019_4_OR_NEWER` and `#endif`.
3. Remove the block of code causing the error.
4. Save your changes.

This should resolve the error messages related to "XboxSeriesOne" and "Ps5".
