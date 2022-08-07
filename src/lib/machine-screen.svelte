<script lang="ts">
    import Dice from "./dice.svelte";
    import type { DiceResult } from "src/models/models";
    import { onMount } from "svelte";


    export let level;

    let diceRotate1 = 0;
    let diceRotate2 = 0;
    let diceRotate3 = 0;

    let socket: WebSocket;

    onMount(() => {
        socket = new WebSocket("wss://localhost:7123/sort");
        
        socket.onmessage = (message) => {
            console.log(message);
            let result = JSON.parse(message.data) as DiceResult;
            diceRotate1 = result.dice1Steps;
            diceRotate2 = result.dice2Steps;
            diceRotate3 = result.dice3Steps;
        };
    });

    export const screen = {
        randomAll(): void {
            let utf8Encode = new TextEncoder();
            let request = utf8Encode.encode(JSON.stringify({sides: 8}));
            socket.send(request);
            // axios.post<DiceResult>("https://localhost:7123/dice/sort", {
            //     sides: 8,
            // }).then(resp => {
            //     diceRotate1 = 0;
            //     diceRotate2 = 0;
            //     diceRotate3 = 0;
            //     setTimeout(() => {
            //         diceRotate1 = resp.data.dice1Steps;
            //         diceRotate2 = resp.data.dice2Steps;
            //         diceRotate3 = resp.data.dice3Steps;
            //     }, 500)
                
            // }).catch(erros => {
            //     console.log(erros);
            //     alert("Api connection error. See console for more details.");
            // });
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