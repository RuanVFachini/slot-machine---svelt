export default function(baseNumber: number) {
    return getRandom(baseNumber);
}

function getRandom(baseNumber: number): number {
    let random = Math.floor(Math.random() * baseNumber);
    return random < 10 ? getRandom(baseNumber) : random;
}