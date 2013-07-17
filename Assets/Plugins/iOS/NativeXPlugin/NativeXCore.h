//
//  NativeXCore.h
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import <Foundation/Foundation.h>
#import "NativeXMonetizationSDK.h"
#import "NativeXAdView.h"
//
//extern UIView * UnityGetGLView();
//extern UIViewController * UnityGetGLViewController();

@interface NativeXCore : NSObject <NativeXMonetizationDelegate, NativeXInAppPurchaseTrackDelegate, NativeXAdViewDelegate >
{
@private
    BOOL showInterstitial;
    CGPoint myPoint;
    
}

+ (NativeXCore*) instance;

-(void)startWithName:(NSString*)name applicationId:(NSString*)appId publisherId:(NSString*)pubId enableLogging:(bool) enableLogging;
-(void)showOfferWall;
-(void)showIncentOfferWall;
-(void)showNonIncentOfferWall;
-(void)showOfferWallFromPoint;
-(void)getAndCacheFeaturedOffer;
-(void)showCachedFeaturedOffer;
-(void)showFeaturedOffer;
-(void)fetchInterstitial:(NSString*)name;
-(void)showInterstitial:(NSString*)name;
-(void)actionTaken:(NSString*)actionId;
-(void)connectWithAppId:(NSString*)appId;
-(void)redeemCurrency;
-(void)trackInAppPurchase:(NativeXInAppPurchaseTrackRecord*)record;
-(void)close;

@property (nonatomic, retain) NativeXAdView *myAdView;
@property (nonatomic, retain) UIView *pointView;
@property (nonatomic) CGPoint bannerPoint;
@property (nonatomic) CGPoint offerWallPoint;
@property (nonatomic) float bannerHeight;
@property (nonatomic) float bannerWidth;
@property (nonatomic, retain) NSDictionary *interstitialDic;
@property (nonatomic, retain) NSString *URL;


@end
