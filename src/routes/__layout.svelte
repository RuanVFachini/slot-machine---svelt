<script lang="ts">
	import '../app.css';
</script>

<main>
	<slot />
</main>

<style>
	main {
		/* flex: 1; */
		/* background-color: rebeccapurple; */
		display: flex;
		justify-content: center;
		align-items: flex-end;
		height: 100vh;
		/* display: flex; */
		/* flex-direction: column; */
		/* justify-items: start; */
		/* height: 100%;
		width: 100%; */
		/* box-sizing: border-box; */
	}
</style>
