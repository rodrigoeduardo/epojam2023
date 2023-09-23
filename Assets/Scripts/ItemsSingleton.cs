using System.Collections;
using System.Collections.Generic;

public class ItemsSingleton {
    // ItemsSingleton keys = ItemsSingleton.Instance;

    private HashSet<string> keys = new HashSet<string>();

    private static ItemsSingleton instance;

    public static ItemsSingleton Instance {
        get {
            if (instance == null) {
                instance = new ItemsSingleton();
            }
            return instance;
        }
    }

    private ItemsSingleton() {}

    // Add key method
    public void addKey(string name) {
        keys.Add(name);
    }

    // Checks if already has key
    public bool hasKey(string name) {
        return keys.Contains(name);
    }

    public void clearKeys() {
        keys = new HashSet<string>();
    }
}
