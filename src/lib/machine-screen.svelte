<script lang="ts">
    import Dice from "./dice.svelte";
    import getRandom from "../utils/random.utils";

    export let level = 15;

    let diceRotate1 = 0;
    let diceRotate2 = 0;
    let diceRotate3 = 0;

    function random(dice: any): number {
        return dice + (getRandom(level) * 45);
    }

    export const screen = {
        randomAll(): void {
            Promise.all([
                diceRotate1 = random(diceRotate1),
                diceRotate2 = random(diceRotate2),
                diceRotate3 = random(diceRotate3),
            ]);
        }
    }
</script>

<div class="screen">
    <div class="screen-image">
        <Dice degre={diceRotate1}></Dice>
    </div>
    <div class="screen-image">
        <Dice degre={diceRotate2}></Dice>
    </div>
    <div class="screen-image">
        <Dice degre={diceRotate3}></Dice>
    </div>
</div>

<style scoped>
    .screen-image {
        flex: 1;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100%;
    }

    .screen {
        /* background-color: blueviolet; */
        /* margin: 30px; */
        flex: 1;
        display: flex;
        padding: 20px;
    }
</style>