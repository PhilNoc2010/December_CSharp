/**
 * Class to represent a queue using an array to store the queued items.
 * Follows a FIFO (First In First Out) order where new items are added to the
 * back and items are removed from the front.
 */
class Queue {
    constructor() {
        this.items = [];
    }

    /**
     * Retrieves the size of this queue.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {number} The length.
     */
    len() {
        return this.items.length;
    }

    /**
     * Adds a new given item to the back of this queue.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @param {any} item The new item to add to the back.
     * @returns {number} The new size of this queue.
     */
    enqueue(item) {
        this.items.push(item);
        return this.items.length;
    }

    /**
     * Removes and returns the first item of this queue.
     * - Time: O(n) linear, due to having to shift all the remaining items to
     *    the left after removing first elem.
     * - Space: O(1) constant.
     * @returns {any} The first item or undefined if empty.
     */
    dequeue() {
        return this.items.shift();
    }

    /**
     * Retrieves the first item without removing it.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {any} The first item or undefined if empty.
     */
    front() {
        return this.items[0];
    }

    /**
     * Returns whether or not this queue is empty.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {boolean}
     */
    isEmpty() {
        return this.items.length === 0;
    }

    print() {
        console.log(this.items);
        return this.items;
    }
}

/* Rebuild the above class using a linked list instead of an array. */

/* 
In order to maintain an O(1) enqueue time complexity like .push with an array
We add a tail to our linked list so we can go directly to the last node
*/

class QueueNode {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class LinkedListQueue {
    constructor() {
        this.head = null;
        this.tail = null;
        this.size = 0;
    }
    /**
     * Retrieves the size of this queue.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {number} The length.
     */
    len() {
        return this.size
    }

    /**
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {boolean} Indicates if the list is empty.
     */
    isEmpty() {
        return this.size === 0

    }

    /**
     * Adds a given val to the back of the queue.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @param {any} val
     * @returns {number} The new size of the queue.
     */
    enqueue(val) {
        const newNode = new QueueNode(val);

        if (this.head === null) {
            this.head = newNode
            this.tail = newNode
        } else {
            this.tail.next = newNode
            this.tail = newNode
        }

        this.size++
        return this.size;

    }

    /**
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {any} The removed item.
     */
    dequeue() {
        if (!this.head) {
            return null
        }
        const removedHead = this.head
        //your code here
        this.head = this.head.next
        if (this.head === null) {
            this.tail = null
        }
        this.size--
        return removedHead.data
    }

    /**
     * Retrieves the first item without removing it.
     * - Time: O(1) constant.
     * - Space: O(1) constant.
     * @returns {any} The first item.
     */
    front() {

        return this.head ? this.head.data : null

    }

    /**
 * Compares this queue to another given queue to see if they are equal.
 * Two queues are equal if they are the same size and have the same node values in order.
 * Use the queue methods we've created.
 * Use no extra array or objects.
 * The queues should be returned to their original order when done.
 * 
 * - Time: O(n^2) quadratic, n = queue length. Quadratic due to dequeue on an
 *     array queue being O(n).
 * - Space: O(1) constant.
 * @param {Queue} q2 The queue to be compared against this queue.
 * @returns {boolean} Whether all the items of the two queues are equal and
 *    in the same order.
 */
    compareQueues(q2) {
        //Your Code Here
        // Note: Our Dequeue method we created returns the data of the node. This can be useful for comparing values.
        // We also want to make sure the queues are returned to the original order after evaluating. (If we were dequeueing, we'd want to enqueue)
        if (this.len() !== q2.len()) {
            return false
        }

        let answer = true
        const origTail = this.tail
        while (this.head !== origTail) {
            const node1 = this.dequeue()
            const node2 = q2.dequeue()
            if (answer && node1 !== node2) {
                answer = false
            }
            this.enqueue(node1)
            q2.enqueue(node2)
        }
        const node1 = this.dequeue()
        const node2 = q2.dequeue()
        if (answer && node1 !== node2) {
            answer = false
        }
        this.enqueue(node1)
        q2.enqueue(node2)

        return answer
    }

    /**
     * Determines whether the sum of the left half of the queue items is equal to
     * the sum of the right half. Avoid indexing the queue items directly via
     * bracket notation, use the queue methods instead for practice.
     * Use no extra array or objects.
     * If queue cannot be evenly divided, return false.
     * The queue should be returned to it's original order when done.
     * - Time: O(n^2) quadratic, n = queue length. Quadratic due to dequeue on an
     *     array queue being O(n).
     * - Space: O(1) constant.
     * @returns {boolean} Whether the sum of the left and right halves is equal.
     */
    isSumOfHalvesEqual() {
        // if the size of the queue is odd, there cannot be two equal halves, so false will be returned.
        if (this.size % 2 !== 0) {
            return false
        }
        //now that we know we have an even number, we will dequeue our queue nodes
        // to determine the sum of the left and right side nodes
        let leftSideSum = 0
        let rightSideSum = 0
        let count = 0
        let length = this.len()
        let halfPoint = length / 2

        while (count < length) {
            const dqNode = this.dequeue()
            if (count < halfPoint) {
                leftSideSum += dqNode
            } else {
                rightSideSum += dqNode
            }
            this.enqueue(dqNode)
            count++
        }

        return leftSideSum == rightSideSum
    }

    /**
     * Enqueues each of the given items.
     * - Time: O(n) linear since enqueue is O(1), n = vals.length.
     * - Space: O(1) constant.
     * @param {Array<any>} vals
     */
    seed(vals) {
        vals.forEach((val) => this.enqueue(val));
    }

    print() {
        let runner = this.head;
        let vals = "";

        while (runner) {
            vals += `${runner.data}${runner.next ? ", " : ""}`;
            runner = runner.next;
        }
        console.log(vals);
        return vals;
    }

}



const arrayQueue = new Queue();
arrayQueue.items = [1, 2, 9, 3, 3, 6];
arrayQueue.print();

const listQueue = new LinkedListQueue();
listQueue.seed([1, 2, 9, 3, 3, 6]);
listQueue.print();