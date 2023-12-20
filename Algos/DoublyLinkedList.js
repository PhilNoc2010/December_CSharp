/**
 * Class to represent a Node for a DoublyLinkedList.
 */
class DLLNode {
    /**
     * Executed when the new keyword is used to construct a new node instance.
     * @param {any} data The data the new node will store.
     */
    constructor(data) {
        this.data = data;
        /**
         * By default a new node instance will not be connected to any other nodes,
         * these properties will be set after instantiation to connect the node to
         * a list. However, the head prev should remain null.
         *
         * @type {DLLNode|null}
         */
        this.prev = null;
        /** @type {DLLNode|null} */
        this.next = null;
    }
}

/**
 * A class to represent a doubly linked list and contain all of it's methods.
 * A doubly linked list is a singly linked list that can be traversed in both
 * directions.
 */
class DoublyLinkedList {
    /**
     * Executed when the new keyword is used to construct a new DoublyLInkedList
     * instance that inherits these methods and properties.
     */
    constructor() {
        // The list is empty to start.
        /** @type {DLLNode|null} */
        this.head = null;
        /** @type {DLLNode|null} */
        this.tail = null;
    }

    /**
     * Converts this list to an array of the node's data.
     * - Time: O(n) linear, n = list length.
     * - Space: O(n) linear, array grows as list length increases.
     * @returns {Array<any>} All the data of the nodes.
     */
    toArray() {
        const vals = [];
        let runner = this.head;

        while (runner) {
            vals.push(runner.data);
            runner = runner.next;
        }
        return vals;
    }

    /**
     * Determines if this list is empty.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {boolean} Indicates if this list is empty.
     */
    isEmpty() {
        return this.head === null;
    }

    /**
     * Creates a new node and adds it at the front of this list.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @param {any} data The data for the new node.
     * @returns {DoublyLinkedList} This list.
     */
    insertAtFront(data) {
        const newNode = new DLLNode(data)
        if (this.isEmpty()) {
            this.head = newNode
            this.tail = newNode
            return this
        }
        newNode.next = this.head

        this.head.prev = newNode
        this.head = newNode
        return this
    }

    /**
     * Creates a new node and adds it at the back of this list.
     * - Time: O(1) constant. No loop is needed since we have direct access to
     *    the tail.
     * - Space: O(1) constant.
     * @param {any} data The data for the new node.
     * @returns {DoublyLinkedList} This list.
     */
    insertAtBack(data) {
        const newNode = new DLLNode(data)
        if (this.isEmpty()) {
            this.head = newNode
            this.tail = newNode
            return this
        }
        newNode.prev = this.tail

        this.tail.next = newNode
        this.tail = newNode
        return this
    }

    /**
     * Adds all the given items to the back of this list.
     * @param {Array<any>} items Items to be added to the back of this list.
     * @returns {DoublyLinkedList} This list.
     */
    insertAtBackMany(items = []) {
        items.forEach((item) => this.insertAtBack(item));
        return this;
    }

    /**
     * Removes the middle node in this list.
     * - Time: O(0.5n) -> O(n) linear, n = list length.
     *    Since it's not constant we simplify it to O(n). Without the early
     *    exists, it would not be 0.5n.
     * - Space: O(1) constant.
     * @returns {any} The data of the removed node.
     */
    removeMiddleNode() {
        // EXTRA
        // Hint: Like when we navigate arrays left and right, we could approach this with two runners. 
        // Example runners: let leftRunner = this.head; let rightRunner = this.tail;
        // If these runners pass each other, it means there is no true middle node. (Such as having a list of 4 Nodes.)
        if (this.isEmpty()) return null
        let leftRunner = this.head
        let rightRunner = this.tail

        while (leftRunner.next != rightRunner) {
            rightRunner = rightRunner.prev
            leftRunner = leftRunner.next
            if (leftRunner === rightRunner) {
                leftRunner.prev.next = rightRunner.next
                return leftRunner.data
            }
        }
        return null
    }

    /**
      * Inserts a new node with the given newVal after the node that has the
      * given targetVal as it's data.
      * - Time: O(?).
      * - Space: O(?).
      * @param {any} targetVal The node data to find.
      * @param {any} newVal Data for the new node.
      * @returns {boolean} Indicates if the new node was added.
      */
    insertAfter(targetVal, newVal) {
        // Your Code Here
        // Start by checking if the Target Value is the Tail of the List
        if (this.tail.data == targetVal) {
            this.insertAtBack(newVal)
            return true
        }

        //create a runner to traverse the list from head to tail
        let runner = this.head
        //this will stop the runner if the tail is reached and the target value is not discovered
        while (runner.next != null) {
            if (runner.data == targetVal) {
                //build the new node
                const newNode = new DLLNode(newVal)
                //set the nodes of newly created node to point to the the values surrouding the runner
                newNode.prev = runner
                newNode.next = runner.next
                //update the next node at the runner's position
                runner.next = newNode
                //update the node after the runner's position to reflect the new node
                runner.next.prev = newNode
                // return a boolean to confirm this was completed correctly
                return true
            }
            runner = runner.next
        }
        return false
    }
    /**
     * Inserts a new node with the given newVal before the node that has the
     * given targetVal as it's data.
     * - Time: O(?).
     * - Space: O(?).
     * @param {any} targetVal The node data to find.
     * @param {any} newVal Data for the new node.
     * @returns {boolean} Indicates if the new node was added.
     */
    insertBefore(targetVal, newVal) {
        // Your Code Here
        if (this.head.data == targetVal) {
            this.insertAtFront(newVal)
            return true
        }

        //create a runner to traverse the list from tail to head
        let runner = this.tail
        //this will stop the runner if the tail is reached and the target value is not discovered
        while (runner.prev != null) {
            if (runner.data == targetVal) {
                //build the new node
                const newNode = new DLLNode(newVal)
                //set the nodes of newly created node to point to the the values surrouding the runner
                newNode.next = runner
                newNode.prev = runner.prev
                //update the next node at the runner's position
                //update the node after the runner's position to reflect the new node
                runner.prev.next = newNode
                runner.prev = newNode
                // return a boolean to confirm this was completed correctly
                return true
            }
            runner = runner.prev
        }
        return false
    }

}

const emptyList = new DoublyLinkedList();

const testList = new DoublyLinkedList().insertAtBack(1).insertAtBack(2).insertAtBack(3).insertAtBack(4).insertAtBack(5);

console.log(testList.removeMiddleNode());
console.log(testList.toArray());
// const singleNodeList = new DoublyLinkedList().insertAtFront(1);
// const biNodeList = new DoublyLinkedList().insertAtBack(1).insertAtBack(2);
// const firstThreeList = new DoublyLinkedList().insertAtBackMany([1, 2, 3]);
// const secondThreeList = new DoublyLinkedList().insertAtBackMany([4, 5, 6]);
// const unorderedList = new DoublyLinkedList().insertAtBackMany([
//     -5, -10, 4, -3, 6, 1, -7, -2,
// ]);
