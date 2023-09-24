using System.Collections;
using System.Collections.Generic;

public class ItemsSingleton {
    // ItemsSingleton keys = ItemsSingleton.Instance;

    private HashSet<char> keys = new HashSet<char>();

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
    public void addKey(char k) {
        keys.Add(k);
    }

    // Checks if already has key
    public bool hasKey(char k) {
        return keys.Contains(k);
    }

    public void clearKeys() {
        keys = new HashSet<char>();
    }
}
