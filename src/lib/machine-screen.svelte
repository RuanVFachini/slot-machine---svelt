<script lang="ts">
    import Dice from "./dice.svelte";
    import axios from "axios";
import type { DiceResult } from "src/models/models";

    export let level = 15;

    let diceRotate1 = 0;
    let diceRotate2 = 0;
    let diceRotate3 = 0;

    export const screen = {
        randomAll(): void {
            axios.post<DiceResult>("https://localhost:7123/dice/sort", {
                sides: 8,
            }).then(resp => {
                diceRotate1 = resp.data.dice1Steps;
                diceRotate2 = resp.data.dice2Steps;
                diceRotate3 = resp.data.dice3Steps;
            }).catch(erros => {
                console.log(erros);
                alert("Api connection error. See console for more details.");
            });
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