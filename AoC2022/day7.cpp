#include <fstream>
#include <vector>
#include <stack>
#include <iostream>
#include <list>
#include <queue>

using namespace std;

class folder {
    private:
        long totalSize;
        long fileSize;
    public:
        folder *parent;
        vector<folder> children;
        vector<long> filesizes;
        vector<string> filenames;
        string name;

        folder(){
            parent = NULL;
            name = "";
            totalSize = 0;
            fileSize = 0;
        }

        void addFile(string name, long size) {
            filenames.push_back(name);
            filesizes.push_back(size);
            fileSize += size;
        }

        void addFolder(string name) {
            folder nf;
            nf.name = name;
            nf.parent = this;
            children.push_back(nf);
        }

        folder* getChild(string name) {
            for (int i = 0; i < children.size(); i++)
            {
                if (children[i].name == name){
                    return &children[i];
                }
            }
            return NULL;
        }

        int TotalSize(){
            if (totalSize == 0){
                totalSize = fileSize;
                for (auto c: children)
                {
                    totalSize += c.TotalSize();
                }
            }            
            return totalSize;
        }


};

int main()
{
    ifstream file("day7.txt");
    vector<string> lines;
    while (file.good())
    {
        string line;
        getline(file, line);
        lines.push_back(line);
    }
    file.close();

    folder tree;
    tree.name = "/";
    folder* node;
    node = &tree;

    for(auto const& line: lines){
        if (line == "$ cd /"){
            node = &tree;
        }
        else if (line == "$ cd .."){
            node = node->parent;
        }
        else if (line == "$ ls") {
            // do nothing
        }
        else if (line.substr(0,5) == "$ cd ") {
            folder* nf = node->getChild(line.substr(5));
            if (nf != NULL){
                node = nf;
            }
        }
        else if (line.substr(0,3) == "dir"){
            node->addFolder(line.substr(4));
        }
        else if (line.length() > 0) {
            int space = line.find(' ');
            int size = stoi(line.substr(0, space));
            string name = line.substr(space + 1);
            node->addFile(name, size);
        }
    }

    queue<folder*> toCheck;
    toCheck.push(&tree);
    long totalSize = 0;
    while (!toCheck.empty()){
        folder* n = toCheck.front();
        toCheck.pop();
        for (int i = 0; i < n->children.size(); i++)
        {
            toCheck.push(&n->children[i]);
        }
        
        if (n->TotalSize() < 100000){
            totalSize += n->TotalSize();
        }

    }

    cout << "Part 1: " << totalSize << endl;

    long diskSpace = 70000000;
    long usedSpace = tree.TotalSize();
    long freeSpace = diskSpace - usedSpace;
    long toFree = 30000000 - freeSpace;

    long smallest = usedSpace;

    toCheck.push(&tree);
    while (!toCheck.empty()){
        folder* n = toCheck.front();
        toCheck.pop();
        for (int i = 0; i < n->children.size(); i++)
        {
            toCheck.push(&n->children[i]);
        }
        
        if (n->TotalSize() < smallest && n->TotalSize() >= toFree){
            smallest = n->TotalSize();
        }

    }

    cout << "Part 2: " << smallest << endl;



}
