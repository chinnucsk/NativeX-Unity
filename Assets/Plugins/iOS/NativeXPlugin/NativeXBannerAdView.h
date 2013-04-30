//
//  NativeXBannerAdView.h
//  NativeXPublisherSdk
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Bozhidar Mihaylov on 12/19/11.
//  Copyright (c) 2011 MentorMate. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol NativeXBannerAdViewDelegate;
@interface NativeXBannerAdView : UIView

@property (nonatomic, unsafe_unretained) id<NativeXBannerAdViewDelegate> delegate;
@property (nonatomic, copy) NSNumber *themeID;

@property (nonatomic, readonly, getter = isAdLoading) BOOL adLoading;
@property (nonatomic, readonly, getter = isAdLoaded) BOOL adLoaded;

+ (CGFloat)defaultBannerHeight;

+ (id)bannerAdViewWithThemeID:(NSNumber *)themeID 
                     delegate:(id<NativeXBannerAdViewDelegate>)delegate 
                        frame:(CGRect)frame;
- (id)initWithThemeID:(NSNumber *)themeID 
             delegate:(id<NativeXBannerAdViewDelegate>)delegate 
                frame:(CGRect)frame;

- (void)reloadAdContent;

@end

@protocol NativeXBannerAdViewDelegate <NSObject>

@required
- (void)didLoadContentForBannerAdView:(NativeXBannerAdView *)adView;
- (void)noAdContentForBannerAdView:(NativeXBannerAdView *)adView;
- (void)bannerAdView:(NativeXBannerAdView *)adView
    didFailWithError:(NSError *)error;

@optional
- (void)dismissActionForBannerAdView:(NativeXBannerAdView *)adView;

@end