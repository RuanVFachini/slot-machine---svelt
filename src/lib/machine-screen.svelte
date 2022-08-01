<script lang="ts">
    import Dice from "./dice.svelte";
    import getRandom from "../utils/random.utils";

    export let level = 15;

    let dice1 = 0;
    let dice2 = 0;
    let dice3 = 0;

    function random(dice: any): number {
        return dice + (getRandom(level) * 90);
    }

    export const screen = {
        randomAll(): void {
            Promise.all([
                dice1 = random(dice1),
                dice2 = random(dice2),
                dice3 = random(dice3),
            ]);
        }
    }
</script>

<div class="screen">
    <div class="screen-image">
        <Dice degre={dice1}></Dice>
    </div>
    <div class="screen-image">
        <Dice degre={dice2}></Dice>
    </div>
    <div class="screen-image">
        <Dice degre={dice3}></Dice>
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