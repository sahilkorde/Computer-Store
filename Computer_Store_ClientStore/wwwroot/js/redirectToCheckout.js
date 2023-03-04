redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51MgkHuSEZcQQ3zkpSfrGYLfmsLgAB0G9uq9IJmD5sZ0MgC2S2FYRHKgQXi6kxk7MtMogRHacCzKEkuMOA80AOyJk00xMxYlHJV");
    stripe.redirectToCheckout({ sessionId: sessionId });
}